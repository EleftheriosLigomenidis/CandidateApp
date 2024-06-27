import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Candidate } from 'src/models/Candidate';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {


  constructor(private http: HttpClient) { }
  private readonly key: string = 'Candidates';
  getCandidates(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(environment.baseUrl + this.key);
  }

  getCandidate(id: number): Observable<Candidate> {
    return this.http.get<Candidate>(environment.baseUrl + this.key + `/${id}`);
  }

  createCandidate(Candidate: Candidate): Observable<Candidate> {
    return this.http.post<Candidate>(environment.baseUrl + this.key, Candidate);
  }

  updateCandidate(Candidate: Candidate): Observable<Candidate> {
    return this.http.put<Candidate>(environment.baseUrl + this.key, Candidate);
  }

  deleteCandidate(id: number): Observable<any> {
    return this.http.delete(environment.baseUrl + this.key + `/${id}`);
  }

}
