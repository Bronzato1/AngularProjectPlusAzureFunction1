import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService
{
  baseUrl = environment.apiUrl;
  baseMessagesUrl = this.baseUrl + 'messages';
  constructor(private http: HttpClient) { }

  getMessage(): Observable<string> {
    return this.http.get<string>(this.baseMessagesUrl).pipe(
      map(res => res['data' as any]),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return throwError(() => new Error(errMessage));
    }
    return throwError(() => error || 'Server error');
  }
}
