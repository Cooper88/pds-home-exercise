import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {PersonService} from '../../services/person.service';
import {PersonViewModel} from "../../models/person-view-model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DepartmentViewModel} from "../../models/department-view-model";
import {DepartmentService} from "../../services/department.service";
import {dateValidator} from "../../validators/dateValidator";

@Component({
  selector: 'person-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnChanges, OnInit {

  @Input() personId: number = 0;
  personForm: FormGroup;
  selectedPersonId: number = 0;

  departmentList: DepartmentViewModel[] = [];

  @Output() submitButtonClicked = new EventEmitter<void>();

  ngOnChanges(changes: SimpleChanges) {
    if (changes['personId']) {
      this.resetPersonEditor();
      const personId: number = changes['personId'].currentValue;
      // console.log('New value:', changes['personId'].currentValue);
      if (personId > 0) {
        this.getPersonById(personId);
      }
    }
  }

  constructor(private fb: FormBuilder,
              private personService: PersonService,
              private departmentService: DepartmentService) {
    this.personForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      department: ['', Validators.required],
      dateOfBirth: ['', [
        Validators.required,
        dateValidator]
      ],
      email: ['', [
        Validators.required,
        Validators.email]
      ],
    });
  }

  ngOnInit(): void {
    this.departmentService.getAll().subscribe({
      next: result => {
        this.departmentList = result;
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

    if (this.selectedPersonId > 0) {

      let payload = {
        id: this.personId,
        firstName: this.personForm.controls['firstname'].value,
        lastName: this.personForm.controls['lastname'].value,
        departmentId: this.personForm.controls['department'].value,
        dateOfBirth: this.personForm.controls['dateOfBirth'].value,
        emailAddress: this.personForm.controls['email'].value
      }

      this.personService.update(payload).subscribe({
        next: () => this.resetPersonEditor(),
        error: (e) => console.error(`Error: ${e}`)
      });

    } else {

      let payload = {
        firstName: this.personForm.controls['firstname'].value,
        lastName: this.personForm.controls['lastname'].value,
        departmentId: this.personForm.controls['department'].value,
        dateOfBirth: this.personForm.controls['dateOfBirth'].value,
        emailAddress: this.personForm.controls['email'].value
      }

      this.personService.add(payload).subscribe({
        next: () => this.resetPersonEditor(),
        error: (e) => console.error(`Error: ${e}`)
      });
    }

    console.log('Form submitted');
  }

  resetPersonEditor() {
    this.personForm.reset();
    this.selectedPersonId = 0;

    this.submitButtonClicked.emit();
  }

}
