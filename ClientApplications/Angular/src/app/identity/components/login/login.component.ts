import { Component, Input, OnInit, OnDestroy, ViewEncapsulation} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';

import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { takeUntil, mergeMap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'environments/environment';
import { fuseAnimations } from '@fuse/animations';
import { JWTToken } from '@DAL/identity-service/models/JWTToken';
import { StorageMap } from '@ngx-pwa/local-storage';
import { UserService } from '@DAL/api/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class LoginComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  subscriptions:Array<Subscription> = new Array<Subscription>();
  private unsubscribe$: Subject<any> = new Subject<any>();

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
      this.loginForm = this.formBuilder.group({
        userPhone: ['', [Validators.required]],
        userPassword: ['', [Validators.required]]
      });
  }

  ngOnDestroy() {    
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  signin() {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    if (!this.loginForm.valid) {
      console.error(this.loginForm.errors);
    }    
    this.subscriptions.push(this.athenticationService
    .login(this.loginForm.value.userPhone,this.loginForm.value.userPassword)
    .pipe(
      takeUntil(this.unsubscribe$),
      mergeMap(token=>this.athenticationService.getDecodedAccessToken(token as JWTToken)),
      mergeMap(identityUser => this.apiService.getUserById(identityUser.userId)),
      mergeMap(user=>this.storage.set(environment.constants.UserStorageKey,user))      
    )
    .subscribe(
      _ => {     
        this.router.navigate(['/workspace/']);
      },
      (error) => {
        this.snackBar.open(error,'',{
          duration: environment.constants.snackBarDuration,
          announcementMessage: error
        });
      }
    ));
  }

  signup() {
    this.router.navigate(['/identity/registration']);
  }
}