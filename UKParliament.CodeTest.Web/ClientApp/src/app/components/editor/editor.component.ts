import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {PersonService} from '../../services/person.service';
import {PersonViewModel} from "../../models/person-view-model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'person-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnChanges {

  @Input() personId: number = 0;
  personForm: FormGroup;
  selectedPersonId: number = 0;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['personId']) {
      const personId: number = changes['personId'].currentValue;
      console.log('New value:', changes['personId'].currentValue);
      if (personId > 0) {
        this.getPersonById(personId);
      }
    }
  }

  constructor(private fb: FormBuilder, private personService: PersonService) {
    this.personForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', [Validators.required]]
    });
  }


  getPersonById(id: number): void {
    this.personService.getById(id).subscribe({
      next: (result) => this.populatePersonForm(result),
      error: (e) => console.error(`Error: ${e}`)
    });
  }

  populatePersonForm(person:PersonViewModel){

    this.personForm.controls['firstname'].setValue(person.firstName);
    this.personForm.controls['lastname'].setValue(person.lastName);

    this.selectedPersonId = person.id;
  }

  onSubmit() {

    let payload = {
      id: this.personId,
      firstName: this.personForm.controls['firstname'].value,
      lastName: this.personForm.controls['lastname'].value,
    }

    this.personService.update(payload).subscribe({
      next: (result) => this.resetPersonEditor(),
      error: (e) => console.error(`Error: ${e}`)
    });

    console.log('Form submitted:');
  }

  resetPersonEditor(){
    this.personForm.reset();
    this.selectedPersonId = 0;
  }
}
