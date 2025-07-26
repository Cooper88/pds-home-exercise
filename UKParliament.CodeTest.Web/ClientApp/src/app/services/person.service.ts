import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonViewModel } from '../models/person-view-model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getById(id: number): Observable<PersonViewModel> {
    return this.http.get<PersonViewModel>(this.baseUrl + `api/person/${id}`)
  }

  getAll(): Observable<PersonViewModel[]> {
    return this.http.get<PersonViewModel[]>(this.baseUrl + 'api/person/all');
  }

  update(person: any): Observable<any> {
    return this.http.put(this.baseUrl + 'api/person/update', JSON.stringify(person), { headers: this.getHeaders() });
  }

  add(person: any): Observable<any> {
    return this.http.post(this.baseUrl + 'api/person/add', JSON.stringify(person), { headers: this.getHeaders() })
  }

  private getHeaders(){
    return {
      'Content-Type': 'application/json',
    }
  }
}
