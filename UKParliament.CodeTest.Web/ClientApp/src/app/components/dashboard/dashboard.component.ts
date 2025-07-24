import {Component, OnInit, ViewChild} from '@angular/core';
import {PersonViewModel} from "../../models/person-view-model";
import {ListComponent} from "../list/list.component";

@Component({
  selector: 'app-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  people: PersonViewModel[] = [];
  selectedPersonId: number = 0;

  @ViewChild(ListComponent) listComponent!: ListComponent;

  refreshPersonList() {
    this.listComponent?.getAll();  // Call Child B's refresh method
  }
  onPersonSelected(personId: number) {
    this.selectedPersonId = personId;
    // console.log('onPersonSelected:', personId);
    // console.log('selectedPersonId ', this.selectedPersonId);
  }

  addNewPerson() {
    this.selectedPersonId = 0;
  }

}
