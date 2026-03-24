import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { Auth } from '../../../core/services/auth';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements OnInit {
  registerForm!: FormGroup;

  constructor(private fb:FormBuilder, private authService:Auth,private router:Router){}
  ngOnInit(){
    this.registerForm = this.fb.group({
    Name:['',Validators.required],
    Email:['',[Validators.required,Validators.email]],
    Password:['',Validators.required],
    Role:['Customer']
  });

  }
  register(){

    if(this.registerForm.invalid){
      return;
    }

    this.authService.register(this.registerForm.value)
      .subscribe({
        next:(res)=>{
          console.log("User registered",res);
          alert("Registration successful");
          this.router.navigate(['/login']);
        },
        error:(err)=>{
          console.log(err);
        }
      });

  }

}