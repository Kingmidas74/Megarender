import { Component, OnInit, OnDestroy, ViewEncapsulation} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { takeUntil, mergeMap} from 'rxjs/operators';
import { Subject, Subscription } from 'rxjs';
import { FormErrorStateMatcher } from '@common/material/form-error-matcher';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'environments/environment';
import { IdentityService } from '@DAL/identity-service/services/identity.service';
import { CreateIdentityCommand } from '@DAL/identity-service/models/commands/create-identity-command';
import * as uuid from 'uuid';
import { ConfirmIdentityCommand } from '@DAL/identity-service/models/commands/confirm-identity-command';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from '@DAL/api/services/user.service';
import { CreateUserCommand } from '@DAL/api/models/commands/create-user-command';
import { StorageMap } from '@ngx-pwa/local-storage';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class RegistrationComponent implements OnInit, OnDestroy {

  registerForm: FormGroup;
  codeForm: FormGroup;
  
  private unsubscribe$: Subject<any> = new Subject<any>();
  
  step = 1;
  userId:string;

  subscriptions:Array<Subscription> = new Array<Subscription>();
  matcher = new FormErrorStateMatcher();
  mobNumberPattern = "^\\d*\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}$";
  passwordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$";
  
  
  

  constructor(private apiService: UserService,
              private authenticationService: AuthenticationService,
              private identityService: IdentityService,
              private formBuilder: FormBuilder,
              private router: Router,
              private storage: StorageMap,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group(
      {
        userPhone: ['', [Validators.required, Validators.pattern(this.mobNumberPattern)]],
        userEmail: ['', [Validators.required, Validators.email]],
        userPassword: ['', [Validators.required, Validators.pattern(this.passwordPattern)]],
        userPasswordConfirm: ['', [Validators.required]],
        userFirstName: ['', Validators.required],
        userSecondName: ['', Validators.required],
        userLastName: ['', Validators.required],
        userBirthdate: [new Date(), Validators.required]
      },
      {
        validator: this.mustMatch('userPassword', 'userPasswordConfirm')
      }
    );

    this.codeForm = this.formBuilder.group(
      {
        userCode: ['', Validators.required]
      }
    );

  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  nextStep() {
    if (!this.registerForm.valid) {
      console.error(this.registerForm.errors);
      return;
    }        
    const command = new CreateIdentityCommand();
    command.email = this.registerForm.value.userEmail;
    command.phone = this.registerForm.value.userPhone;
    command.password = this.registerForm.value.userPassword;
    command.confirmPassword = this.registerForm.value.userPasswordConfirm;
    command.id = uuid.v4();
    
    this.identityService
        .createIdentity(command)
        .pipe(
          takeUntil(this.unsubscribe$)          
        )
        .subscribe(
          _ => {
            this.step=2;
            this.userId = command.id;
          },
          (error) => {
            this._snackBar.open(error,'',{
              duration: environment.constants.snackBarDuration,
              announcementMessage: error
            });
          }
        );
  }



  signup() {
    if (!this.codeForm.valid) {
      console.error(this.codeForm.errors);
      return;
    }    
    const confirmIdentityCommand = new ConfirmIdentityCommand();
    confirmIdentityCommand.id=this.userId;
    confirmIdentityCommand.code = this.codeForm.value.userCode;

    const createUserCommand = new CreateUserCommand();
    createUserCommand.id=this.userId;
    createUserCommand.birthdate=this.registerForm.value.userBirthdate;
    createUserCommand.firstName=this.registerForm.value.userFirstName; 
    createUserCommand.secondName=this.registerForm.value.userSecondName;
    createUserCommand.surName=this.registerForm.value.userLastName;

    this.identityService.confirmIdentity(confirmIdentityCommand).pipe(
      mergeMap(_=>this.authenticationService.login(this.registerForm.value.userPhone, this.registerForm.value.userPassword)),
      mergeMap(_=>this.apiService.createUser(createUserCommand)),
      mergeMap(user=>this.storage.set(environment.constants.UserStorageKey,user))      
    ).subscribe(
      _=>this.router.navigate(['/workspace/']),
      error=> () => {
        this._snackBar.open(error,'',{
          duration: environment.constants.snackBarDuration,
          announcementMessage: error
        });
      }
    );
  }

  signin() {
    this.router.navigate(['/identity/']);
  }

  mustMatch(controlName: string, matchingControlName: string) {
      return (formGroup: FormGroup) => {
          const control = formGroup.controls[controlName];
          const matchingControl = formGroup.controls[matchingControlName];

          if (matchingControl.errors && !matchingControl.errors.mustMatch) {           
              return;
          }
          if (control.value !== matchingControl.value) {
              matchingControl.setErrors({ mustMatch: true });
          } else {
              matchingControl.setErrors(null);
          }
      }
  }

}