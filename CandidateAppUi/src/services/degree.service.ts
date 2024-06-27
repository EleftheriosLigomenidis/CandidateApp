import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Degree } from 'src/models/Degree';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DegreeService {


  constructor(private http: HttpClient) { }
  private readonly key: string = 'degrees';
  getDegrees(): Observable<Degree[]> {
    return this.http.get<Degree[]>(environment.baseUrl + this.key);
  }

  getDegree(id: number): Observable<Degree> {
    return this.http.get<Degree>(environment.baseUrl + this.key + `/${id}`);
  }

  createDegree(Degree: Degree): Observable<Degree> {
    return this.http.post<Degree>(environment.baseUrl + this.key, Degree);
  }

  updateDegree(Degree: Degree): Observable<Degree> {
    return this.http.put<Degree>(environment.baseUrl + this.key, Degree);
  }

  deleteDegree(id: number): Observable<any> {
    return this.http.delete(environment.baseUrl + this.key + `/${id}`);
  }
}
