import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig, _SnackBarContainer } from '@angular/material/snack-bar';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private formBuilder : FormBuilder , private userService : UserService , private snackBar : MatSnackBar) { }
  
  //add regex for password  
  get userName()
  {
    return this.userRegistration.get('userName')
  }
  get email()
  {
    return this.userRegistration.get('email')
  }
  get password()
  {
    return this.userRegistration.get('passwords').get('password')
  }
  get confirmPassword()
  {
    return this.userRegistration.get('passwords').get('confirmPassword')
  }
  comparePasswords(formBuilder : FormGroup)
  {
    let confirmPasswordControl = formBuilder.get('confirmPassword')
    if(confirmPasswordControl.errors == null || 'passwordMismatch' in confirmPasswordControl.errors){
      if(formBuilder.get('password').value != confirmPasswordControl.value)
      {
        confirmPasswordControl.setErrors({passwordMismatch : true})
      }
      else
      {
        confirmPasswordControl.setErrors(null)
      }
    }
  }
  register()
  {
    var userForm = {
      'userName' : this.userRegistration.value.userName,
      'email' : this.userRegistration.value.email , 
      'password' : this.userRegistration.value.passwords.password,
      'fullName' : this.userRegistration.value.fullName
    }
    this.userService.register(userForm).subscribe(data => {
      console.log(data)
      let configSnackBar = new MatSnackBarConfig()
      configSnackBar.panelClass = "snackbar-danger"
      configSnackBar.duration = 10000;

      if(data.succeeded == true)
      this.snackBar.open('The account was created.You can login now!' , "" , {duration : 3000 , panelClass : 'snackbar-success'})
      else{
        let errors = ''
        data.errors.forEach(element => {
          errors += element.description 
          this.snackBar.open(errors , "" , configSnackBar)
        });
        
      }
    })
    
  }

  

  userRegistration = this.formBuilder.group({
    'userName' : ['' , Validators.required],
    'email' : ['' , Validators.required],
     'passwords' : this.formBuilder.group({
      'password' : ['',[Validators.required ]],
      'confirmPassword' : ['',Validators.required]  
     }, {validator : this.comparePasswords}),
    
  }

  )
  ngOnInit(): void {
  }

}
