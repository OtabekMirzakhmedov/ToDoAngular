import { Component, Inject } from '@angular/core';
import { Form, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { UserService } from '../user-service/user.service';
import { HttpClient } from '@angular/common/http';

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
    UserDetails: appUser;
    initialText: string = '';
    toDoList: todoItem[] = [];
    step = 1;
    form: FormGroup;
    tempSubStep: Steps[] = [];
    editForm: FormGroup;
    editMode = false;
    activeToDo: number;
    takeNote = false;
    userId: string = undefined;

    constructor(private fb: FormBuilder, private router: Router, private service: UserService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

   

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

        if (this.loggedIn()) {
            this.service.getUserProfile().subscribe(
                res => {
                    this.UserDetails = res;
                    this.userId = res.id;
                    this.http.get<todoItem[]>(this.baseUrl + 'api/ToDo/' + res.id).subscribe(result => {
                        this.toDoList = result;
                        console.log(result);
                    }, error => console.log(error));
                  
                },
                err => {
                    console.log(err);
                },
            );
        }

        //if (this.loggedIn() ) {
        //    this.http.get<todoItem[]>(this.baseUrl + 'api/ToDo/' + 'f5953dd5-270d-4627-85f6-9c15bde84fe8').subscribe(result => {
        //        this.toDoList = result;
        //        console.log(result);
        //    }, error => console.log(error));
        //}

    }
    TakeNote() {
        this.takeNote = true;
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
        this.takeNote = false;
    }

    loggedIn() {
        if (localStorage.getItem('token') != null) {

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
    }

    onSubmit() {
        console.log(this.form.value);
        this.step = 1;
        while (this.steps().length !== 0) {
            if (this.steps().at(0).value.stepText.length > 0) {
                this.tempSubStep.push({ stepText: this.steps().at(0).value.stepText, isFinished: false });
            }
            this.steps().removeAt(0)
        }
        console.log(this.tempSubStep);
        //this.toDoList.push({
        //    id: 0,
        //    text: this.form.value.title,
        //    isCompleted: false,
        //    createdAt: new Date(),
        //    deadline: this.form.value.deadline,
        //    appUserId: this.UserDetails.id,
        //    progress: 0,
        //    steps: this.tempSubStep
        //});
        this.http.post<todoItem>(this.baseUrl + 'api/ToDo/', {
            id: 0,
            text: this.form.value.title,
            isCompleted: false,
            createdAt: new Date(),
            deadline: this.form.value.deadline,
            appUserId: this.UserDetails.id,
            progress: 0,
            steps: this.tempSubStep
        }).subscribe((res) => this.toDoList.push(res));


    
        this.tempSubStep = [];
        this.form.reset();
        this.takeNote = false;
        
    }


    onSubCheck(ob: MatCheckboxChange, i: number, j: number) {
        console.log(this.toDoList[i]);
        console.log(this.toDoList[i].steps[j] + ' ' + ob.checked);
        this.toDoList[i].steps[j].isFinished = ob.checked;
    }
    edit(i: number) {
        while (this.stepsEdit().length !== 0) {
            this.stepsEdit().removeAt(0)
        }
        this.editForm.get('titleEdit').setValue(this.toDoList[i].text);
        this.editForm.get('deadlineEdit').setValue(this.toDoList[i].deadline);

        for (let sub of this.toDoList[i].steps) {
            
            this.stepsEdit().push(this.newStepEdit(sub.stepText));

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

    Delete(i: number) {
        this.http.delete(this.baseUrl + 'api/ToDo/' + this.toDoList[i].id).subscribe();
        this.toDoList.splice(i, 1);
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
                this.tempSubStep.push({ stepText: this.stepsEdit().at(0).value.stepTextEdit, isFinished: false});
            }
            this.stepsEdit().removeAt(0)
        }
        this.toDoList[i].steps = this.tempSubStep;
        this.http.put<todoItem>(this.baseUrl + 'api/ToDo/' + this.toDoList[i].id, this.toDoList[i]).subscribe();
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
    id: number;
    text: string;
    isCompleted: boolean;
    appUserId: string;
    createdAt: Date;
    deadline: Date | null;
    progress: number;
    steps: Steps[];
}

interface Steps {
    
    stepText: string;
    isFinished: boolean;

}

interface appUser {
    id: string;
    fullName: string;
}





