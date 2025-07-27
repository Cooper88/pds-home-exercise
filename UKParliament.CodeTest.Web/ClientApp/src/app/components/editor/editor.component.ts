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
    firstname: ['',
      [
        Validators.required,
        Validators.maxLength(20)
      ]
    ],
    lastname: ['',
      [
        Validators.required,
        Validators.maxLength(20)
      ]
    ],
    department: ['',
      Validators.required
    ],
    dateOfBirth: ['', [
      Validators.required,
      presentOrPastDateValidator
    ]
    ],
    email: ['', [
      Validators.required,
      Validators.maxLength(30),
      Validators.email
    ]
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
      this.resetPersonEditorForm();
      const id: number = changes['personId'].currentValue;
      if (id > 0) {
        this.getPersonById(id);
      } else {
        this.selectedPersonId = 0;
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
        next: () => {
          this.resetPersonEditorForm();
          this.submitButtonClicked.emit();
        },
        error: result => {
          this.processServerErrors(result);
        }
      });

    } else {

      this.personService.add(payload).subscribe({
        next: () => {
          this.resetPersonEditorForm();
          this.submitButtonClicked.emit();
        },
        error: result => {
          this.processServerErrors(result);
        }
      });

    }

  }

  resetPersonEditorForm() {
    this.personForm.reset();
    this.personForm.controls['department'].setValue(this.departmentList[0]?.id);
  }

  processServerErrors(data: any) {

    const serverErrors = new Map<string, string>();
    serverErrors.set('FirstName', 'firstname');
    serverErrors.set('LastName', 'lastname');
    serverErrors.set('DepartmentId', 'department');
    serverErrors.set('DateOfBirth', 'dateOfBirth');
    serverErrors.set('EmailAddress', 'email');

    serverErrors.forEach((value, key) => {
      if (data.error.errors?.[`${key}`]?.length > 0) {
        this.personForm.get(`${value}`)?.setErrors({serverError: data.error.errors?.[`${key}`][0]});
      }
    });

  }

}
