import { Component } from '@angular/core';
import { Form, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { UserService } from '../user-service/user.service';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    notifyme = false;
    panelOpenState = false;
    calendarchosen = false;
    selectedCar: string;
    selected: Date | null;
    UserDetails;
    initialText: string = '';
    toDoList: todoItem[] = [];
    step = 1;
    form: FormGroup;
    tempSubStep: Steps[] = [];
    editFrom: FormGroup;

    

    constructor(private fb: FormBuilder, private router: Router, private service: UserService) { }

   

    ngOnInit(): void {
        this.form = this.fb.group({
            title: [this.initialText, Validators.compose([Validators.minLength(3), Validators.maxLength(255)])],
            steps: this.fb.array([]),
            deadline: this.selected
        });
        this.editFrom = this.form;
    }
    get f() {
        return this.form.controls;
    }
    steps(): FormArray {
        return this.form.get("steps") as FormArray
    }
    newStep(): FormGroup {
        return this.fb.group({
            stepText: this.initialText
        })
    }

    addStep() {
        this.steps().push(this.newStep());
    }

    removeStep(i: number) {
        this.steps().removeAt(i);
    }  

    Cancel() {
        while (this.steps().length !== 0) {
            this.steps().removeAt(0)
        }
        this.form.reset();
    }

    loggedIn() {
        if (localStorage.getItem('token') != null) {
            this.service.getUserProfile().subscribe(
                res => {
                    this.UserDetails = res;
                },
                err => {
                    console.log(err);
                },
            );
            return true;

        }
        else {
            return false;
        }
    }

    Logout() {
        localStorage.removeItem('token');
    }


    notify() {
        if (this.notifyme) {
            this.notifyme = false;
            console.log(this.notifyme);
        }
        else {
            this.notifyme = true;
            console.log(this.notifyme);
        }
    }

    calendarOpen() {
        this.calendarchosen = true;
        console.log(this.calendarchosen)
    }
    calendarClose() {
        this.calendarchosen = false;
        console.log(this.calendarchosen)
    }

    onSubmit() {
        console.log(this.form.value);
        this.step = 1;
        while (this.steps().length !== 0) {
            if (this.steps().at(0).value.stepText.length > 0) {
                this.tempSubStep.push({ subStepText: this.steps().at(0).value.stepText, subStepDone: true });
            }
            this.steps().removeAt(0)
        }
        console.log(this.tempSubStep);
        this.toDoList.push({
            text: this.form.value.title,
            done: false,
            deadline: this.form.value.deadline,
            subStep: this.tempSubStep
        });
        console.log(this.form.get('title'))
    
        this.tempSubStep = [];
        this.form.reset();
        
    }
    setStep(num: number) {
        this.step = 0;
    }
    statusChange(index: number) {
        console.log(index);
        this.toDoList[index].done = true;
    }

    onSubCheck(ob: MatCheckboxChange, i: number, j: number) {
        console.log(this.toDoList[i]);
        console.log(this.toDoList[i].subStep[j] + ' ' + ob.checked);
        this.toDoList[i].subStep[j].subStepDone = ob.checked;
    }



 

}





interface todoItem {
    text: string;
    done: boolean;
    deadline: Date | null;
    subStep: Steps[];
}

interface Steps {
    subStepText: string;
    subStepDone: boolean;
}





