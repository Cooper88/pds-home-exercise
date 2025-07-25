import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {PersonService} from '../../services/person.service';
import {PersonViewModel} from "../../models/person-view-model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DepartmentViewModel} from "../../models/department-view-model";
import {DepartmentService} from "../../services/department.service";
import {presentOrPastDateValidator} from "../../validators/presentOrPastDateValidator";

@Component({
  selector: 'person-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnChanges, OnInit {

  @Input() personId: number = 0;

  personForm: FormGroup = this.fb.group({
    firstname: ['', Validators.required],
    lastname: ['', Validators.required],
    department: ['', Validators.required],
    dateOfBirth: ['', [
      Validators.required,
      presentOrPastDateValidator]
    ],
    email: ['', [
      Validators.required,
      Validators.email]
    ],
  });

  selectedPersonId: number = 0;
  departmentList: DepartmentViewModel[] = [];

  @Output() submitButtonClicked = new EventEmitter<void>();

  constructor(private fb: FormBuilder,
              private personService: PersonService,
              private departmentService: DepartmentService) {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['personId']) {
      this.resetPersonEditor();
      const personId: number = changes['personId'].currentValue;
      if (personId > 0) {
        this.getPersonById(personId);
      }
    }
  }

  ngOnInit(): void {
    this.departmentService.getAll().subscribe({
      next: result => {
        this.departmentList = result;
        this.personForm.controls['department'].setValue(this.departmentList[0].id);
      },
      error: (e) => console.error(`Error: ${e}`)
    })
  }

  getPersonById(id: number): void {
    this.personService.getById(id).subscribe({
      next: (result) => this.populatePersonForm(result),
      error: (e) => console.error(`Error: ${e}`)
    });
  }

  populatePersonForm(person: PersonViewModel) {

    this.personForm.controls['firstname'].setValue(person.firstName);
    this.personForm.controls['lastname'].setValue(person.lastName);
    this.personForm.controls['department'].setValue(person.departmentId)
    this.personForm.controls['dateOfBirth'].setValue(person.dateOfBirth);
    this.personForm.controls['email'].setValue(person.emailAddress);

    this.selectedPersonId = person.id;
  }

  onSubmit() {

    const payload: PersonViewModel = {
      id: this.selectedPersonId,
      firstName: this.personForm.controls['firstname'].value,
      lastName: this.personForm.controls['lastname'].value,
      departmentId: this.personForm.controls['department'].value,
      dateOfBirth: this.personForm.controls['dateOfBirth'].value,
      emailAddress: this.personForm.controls['email'].value
    }

    if (this.selectedPersonId > 0) {

      this.personService.update(payload).subscribe({
        next: () => this.resetPersonEditor(),
        error: result => {
          this.processServerErrors(result);
        }
      });

    } else {

      this.personService.add(payload).subscribe({
        next: () => this.resetPersonEditor(),
        error: result => {
          this.processServerErrors(result);
        }
      });

    }

  }

  resetPersonEditor() {
    this.personForm.reset();
    this.selectedPersonId = 0;
    this.personForm.controls['department'].setValue(this.departmentList[0].id);

    this.submitButtonClicked.emit();
  }

  processServerErrors(data: any) {

    if (data.error.errors?.FirstName?.length > 0) {
      this.personForm.get('firstname')?.setErrors({serverError: data.error.errors?.FirstName[0]});
    }

    if (data.error.errors?.LastName?.length > 0) {
      this.personForm.get('lastname')?.setErrors({serverError: data.error.errors?.LastName[0]});
    }

    if (data.error.errors?.DateOfBirth?.length > 0) {
      this.personForm.get('dateOfBirth')?.setErrors({serverError: data.error.errors?.DateOfBirth[0]});
    }

    if (data.error.errors?.EmailAddress?.length > 0) {
      this.personForm.get('email')?.setErrors({serverError: data.error.errors?.EmailAddress[0]});
    }

    if (data.error.errors?.DepartmentId?.length > 0) {
      this.personForm.get('department')?.setErrors({serverError: data.error.errors?.DepartmentId[0]});
    }

  }

}
