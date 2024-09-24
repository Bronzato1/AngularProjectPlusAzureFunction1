import { Component, Inject, OnInit } from '@angular/core';
import { OktaAuth } from '@okta/okta-auth-js';
import { OktaAuthStateService, OKTA_AUTH } from '@okta/okta-angular';
import { AuthState } from '@okta/okta-auth-js';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {ViewEncapsulation} from '@angular/core';
import { filter, map, Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit
{
  userName$!: Observable<string>;
  
  title = 'AngularProjectPlusAzureFunction1';

  isAuthenticated: boolean = false;

  constructor(
    @Inject(OKTA_AUTH) public oktaAuth: OktaAuth,
    private oktaAuthStateService: OktaAuthStateService,
    private modalService: NgbModal
  ) { }

  async ngOnInit()
  {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    this.oktaAuthStateService.authState$.subscribe(
      (response) => {
          this.isAuthenticated = response.isAuthenticated ?? false;
      }
    ); 
    this.userName$ = this.oktaAuthStateService.authState$.pipe(
      filter((authState: AuthState) => !!authState && !!authState.isAuthenticated),
      map((authState: AuthState) => authState.idToken?.claims.name ?? '')
    );      
  }

  public open(modal: any): void {
    this.modalService.open(modal);
  }

  login()
  {
    this.oktaAuth.signInWithRedirect();
  }

  logout()
  {
    this.oktaAuth.signOut();
  }
}
