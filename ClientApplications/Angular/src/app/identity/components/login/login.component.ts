import { Component, Input, OnInit, OnDestroy} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';

import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  subscriptions:Array<Subscription> = new Array<Subscription>();
  private unsubscribe$: Subject<any> = new Subject<any>();
  
  constructor(private athenticationService:AuthenticationService,
              private formBuilder: FormBuilder,
              private router: Router,
              private snackBar: MatSnackBar) {}

  form: FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        userPhone: ['', [Validators.required]],
        userPassword: ['', [Validators.required]]
      }
    );
  }

  ngOnDestroy() {    
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  

  get userPhone() { return this.form.value.userPhone; }
  get userPassword() { return this.form.value.userPassword; }

  submit() {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    if (!this.form.valid) {
      console.error(this.form.errors);
    }    
    this.subscriptions.push(this.athenticationService
    .login(this.userPhone,this.userPassword)
    .pipe(
      takeUntil(this.unsubscribe$)          
    )
    .subscribe(
      (data)=> {        
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
  
  @Input() error: string | null;
}
