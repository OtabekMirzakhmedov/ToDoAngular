import { Component, Inject, OnInit } from '@angular/core';
import { UserService } from '../user-service/user.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    constructor(private service: UserService, private toastr: ToastrService, private actRoute: ActivatedRoute,
        private router: Router, @Inject('BASE_URL') private baseUrl: string    ) { }

    ngOnInit() {
        this.service.formModel.reset();
    }

    onSubmit() {
        this.service.register().subscribe(
            (res: any) => {
                if (res.succeeded) {
                    this.service.formModel.reset();
                    this.toastr.success('New user created!', 'Registration successful.');
                    this.router.navigate(['/login'])
                    
                } else {
                    res.errors.forEach(element => {
                        switch (element.code) {
                            case 'DuplicateUserName':
                                this.toastr.error('Username is already taken', 'Registration failed.');
                                break;

                            default:
                                this.toastr.error(element.description, 'Registration failed.');
                                break;
                        }
                    });
                }
            },
            err => {
                console.log(err);
            }
        );
    }

}
