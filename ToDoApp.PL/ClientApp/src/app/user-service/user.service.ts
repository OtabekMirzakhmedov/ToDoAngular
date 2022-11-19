import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

    constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    formModel = this.fb.group({
        UserName: ['', Validators.required],
        Email: ['', Validators.email],
        FullName: [''],
        Passwords: this.fb.group({
            Password: ['', [Validators.required, Validators.minLength(4)]],
            ConfirmPassword: ['', Validators.required]
        }, { validator: this.comparePasswords })

    });

    comparePasswords(fb: FormGroup) {
        let confirmPswrdCtrl = fb.get('ConfirmPassword');
        //passwordMismatch
        //confirmPswrdCtrl.errors={passwordMismatch:true}
        if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
            if (fb.get('Password').value != confirmPswrdCtrl.value)
                confirmPswrdCtrl.setErrors({ passwordMismatch: true });
            else
                confirmPswrdCtrl.setErrors(null);
        }
    }

    register() {
        var body = {
            UserName: this.formModel.value.UserName,
            Email: this.formModel.value.Email,
            FullName: this.formModel.value.FullName,
            Password: this.formModel.value.Passwords.Password
        };
        return this.http.post(this.baseUrl + 'api/User/register', body);
    }

    login(formData) {
        return this.http.post(this.baseUrl + 'api/User/login', formData);
    }

    getUserProfile() {
        var tokenheader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('token') });
        return this.http.get(this.baseUrl + 'api/User/profile', {headers : tokenheader});
    }
}
