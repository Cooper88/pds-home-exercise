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
    this.getAll();
  }

  editPerson(personId: number): void {
    this.personIdEmitted.emit(personId);
  }

  getAll() {
    this.personService.getAll().subscribe({
      next: (persons => this.people = persons),
      error: (error) => console.log(error),
    });
  }

}
