import { Component, Input, OnInit, OnDestroy, ViewEncapsulation, ElementRef, ViewChild, AfterViewInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';

import { Router } from '@angular/router';
import { fromEvent, Subject, Subscription } from 'rxjs';
import { takeUntil, mergeMap, debounceTime, distinctUntilChanged, tap, filter } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'environments/environment';
import { fuseAnimations } from '@fuse/animations';
import { JWTToken } from '@DAL/identity-service/models/JWTToken';
import { StorageMap } from '@ngx-pwa/local-storage';
import { UserService } from '@DAL/api/services/user.service';
import { CleanSubscriptions } from '@common/shared-utils/clean-subscriptions';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { CreateUserCommand } from '@DAL/api/models/commands/create-user-command';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : [//fuseAnimations,  
    trigger('codeShow', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
@CleanSubscriptions()
export class LoginComponent implements OnInit {

  phoneForm: FormGroup;
  codeForm: FormGroup;
  subscriptions:Array<Subscription> = new Array<Subscription>();
  private unsubscribe$: Subject<any> = new Subject<any>();
  mobNumberPattern = "^\\d*\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}$";

  codeIsSend:boolean = false;


  @ViewChild('phone', {static:false}) userPhone: ElementRef;
  @ViewChild('code') userCode: ElementRef;
  userId: string;

  constructor(        
    private athenticationService:AuthenticationService,
    private formBuilder: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
    private storage: StorageMap,
    private apiService: UserService
  )
  {
  }

  ngOnInit(): void
  {
      this.phoneForm = this.formBuilder.group({
        userPhone: ['', [Validators.required, Validators.pattern(this.mobNumberPattern)]],
      });

      this.codeForm = this.formBuilder.group({
        userCode: ['', [Validators.required]]
      });
  }


  checkPhone() {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    if(!this.phoneForm.controls['userPhone'].valid)
    {
      console.log(this.phoneForm.controls['userPhone'].errors);
      return;
    }
    this.subscriptions.push(
      this.athenticationService.sendCode(this.phoneForm.controls['userPhone'].value)        
        .subscribe(response => {
          this.userId = response.id;
          this.codeIsSend=this.userId.length>0;
        })
    )
    
  }

  signin() {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    if(!this.codeForm.controls['userCode'].valid)
    {
      console.log(this.codeForm.controls['userCode'].errors);
      return;
    }
    
    this.subscriptions.push(
      this.athenticationService.verifyCode(this.userId, this.codeForm.controls["userCode"].value)
      .pipe(
        mergeMap(response => {
          return this.athenticationService.login(this.phoneForm.controls['userPhone'].value, response.password)
        }),
        mergeMap(token=>this.athenticationService.getDecodedAccessToken(token as JWTToken)),
        mergeMap(identityUser=>{
                const createUserCommand = new CreateUserCommand();
                createUserCommand.id = identityUser.userId;
                return this.apiService.createUser(createUserCommand)
              }),
        mergeMap(user=>this.storage.set(environment.constants.UserStorageKey,user))
      )
      .subscribe(
        _=> {
          this.router.navigate(['/workspace/']);
        },
        (error) => {
          this.snackBar.open(error,'',{
            duration: environment.constants.snackBarDuration,
            announcementMessage: error
          })
        }
      )
    );
  }
}