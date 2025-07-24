import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {PersonService} from '../../services/person.service';
import {PersonViewModel} from "../../models/person-view-model";

@Component({
  selector: 'person-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  @Output() personIdEmitted = new EventEmitter<number>();

  people: PersonViewModel[] = [];

  constructor(private personService: PersonService) {
  }

  ngOnInit(): void {
    // Get all people
    this.personService.getAll().subscribe({
      next: (persons => this.people = persons),
      error: (error) => console.log(error),
      complete: () => {console.log(this.people)}
    });
  }

  editPerson(personId: number): void {
    console.log('clicked ', personId);
    this.personIdEmitted.emit(personId);
  }

  // getPersonById(id: number): void {
  //   this.personService.getById(id).subscribe({
  //     next: (result) => console.info(`User returned: ${JSON.stringify(result)}`),
  //     error: (e) => console.error(`Error: ${e}`)
  //   });
  // }
}
