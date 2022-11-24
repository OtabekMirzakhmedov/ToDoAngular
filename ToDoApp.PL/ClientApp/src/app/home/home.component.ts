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
    editForm: FormGroup;
    editMode = false;
    activeToDo: number;
    

    constructor(private fb: FormBuilder, private router: Router, private service: UserService) { }

   

    ngOnInit(): void {
        this.form = this.fb.group({
            title: [this.initialText, Validators.compose([Validators.minLength(3), Validators.maxLength(255)])],
            steps: this.fb.array([]),
            deadline: this.selected
        });
        this.editForm = this.fb.group({
            titleEdit: '',
            stepsEdit: this.fb.array([]),
            deadlineEdit: this.selected
        });
    }
    get f() {
        return this.form.controls;
    }
    steps(): FormArray {
        return this.form.get("steps") as FormArray
    }
    stepsEdit(): FormArray {
        return this.editForm.get("stepsEdit") as FormArray
    }
    newStep(): FormGroup {
        return this.fb.group({
            stepText: this.initialText
        })
    }
    newStepEdit(sometext:string): FormGroup {
        return this.fb.group({
            stepTextEdit: sometext
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
                this.tempSubStep.push({ subStepText: this.steps().at(0).value.stepText, subStepDone: false });
            }
            this.steps().removeAt(0)
        }
        console.log(this.tempSubStep);
        this.toDoList.push({
            text: this.form.value.title,
            done: false,
            deadline: this.form.value.deadline,
            progress: 0,
            subStep: this.tempSubStep
        });
    
        this.tempSubStep = [];
        this.form.reset();
        
    }


    onSubCheck(ob: MatCheckboxChange, i: number, j: number) {
        console.log(this.toDoList[i]);
        console.log(this.toDoList[i].subStep[j] + ' ' + ob.checked);
        this.toDoList[i].subStep[j].subStepDone = ob.checked;
    }
    edit(i: number) {
        while (this.stepsEdit().length !== 0) {
            this.stepsEdit().removeAt(0)
        }
        this.editForm.get('titleEdit').setValue(this.toDoList[i].text);
        this.editForm.get('deadlineEdit').setValue(this.toDoList[i].deadline);

        for (let sub of this.toDoList[i].subStep) {
            
            this.stepsEdit().push(this.newStepEdit(sub.subStepText));

        }

        //this.toDoList[i].subStep.forEach((x) => {
        //    var tempStepEditform = this.fb.group({
        //        stepTextEdit: x.subStepText
        //    });
        //    this.stepsEdit().push(tempStepEditform);
        //});
        this.activeToDo = i;
        this.editMode = true;

    }
    cancelEdit() {
        this.activeToDo = -1;
    }
    Update(i:number) {
        console.log(this.editForm.value);
        this.toDoList[i].deadline = this.editForm.value.deadlineEdit;
        this.toDoList[i].text = this.editForm.value.titleEdit;
        while (this.stepsEdit().length !== 0) {
            if (this.stepsEdit().at(0).value.stepTextEdit.length > 0) {
                this.tempSubStep.push({ subStepText: this.stepsEdit().at(0).value.stepTextEdit, subStepDone: false});
            }
            this.stepsEdit().removeAt(0)
        }
        this.toDoList[i].subStep = this.tempSubStep;
        this.tempSubStep = [];
        this.activeToDo = -1;
    }

    addStepEdit() {
        this.stepsEdit().push(this.newStepEdit(this.initialText));
    }

    removeStepEdit(i: number) {
        this.stepsEdit().removeAt(i);
    } 

    progressChange(i: number, event) {
        this.toDoList[i].progress = event.value;
    }



 

}





interface todoItem {
    text: string;
    done: boolean;
    deadline: Date | null;
    progress: number;
    subStep: Steps[];
}

interface Steps {
    subStepText: string;
    subStepDone: boolean;
}





