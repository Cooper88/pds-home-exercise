import {Component, OnInit} from '@angular/core';
import {PersonService} from '../../services/person.service';
import {PersonViewModel} from "../../models/person-view-model";

@Component({
  selector: 'app-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  people: PersonViewModel[] = [];
  selectedPersonId: number = 0;

  constructor(private personService: PersonService) {
  }

  ngOnInit(): void {
    // Get all people
    // this.personService.getAll().subscribe({
    //   next: (persons => this.people = persons),
    //   error: (error) => console.log(error),
    //   complete: () => {console.log(this.people)}
    // });
  }

  // getPersonById(id: number): void {
  //   this.personService.getById(id).subscribe({
  //     next: (result) => console.info(`User returned: ${JSON.stringify(result)}`),
  //     error: (e) => console.error(`Error: ${e}`)
  //   });
  // }

  onPersonSelected(personId: number) {
    this.selectedPersonId = personId;
    console.log('onPersonSelected:', personId);
    console.log('selectedPersonId ', this.selectedPersonId);
  }
}
