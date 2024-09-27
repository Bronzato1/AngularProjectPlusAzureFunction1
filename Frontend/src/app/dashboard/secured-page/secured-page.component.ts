import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OktaAuthStateService } from '@okta/okta-angular';
import { AuthState } from '@okta/okta-auth-js';
import { filter, map, Observable } from 'rxjs';

@Component({
  selector: 'app-secured-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './secured-page.component.html',
  styleUrl: './secured-page.component.scss'
})
export class SecuredPageComponent implements OnInit {

  userName$!: Observable<string>;

  constructor (public oktaAuthStateService: OktaAuthStateService) { }

  async ngOnInit() 
  {
    this.userName$ = this.oktaAuthStateService.authState$.pipe(
      filter((authState: AuthState) => !!authState && !!authState.isAuthenticated),
      map((authState: AuthState) => authState.idToken?.claims.name ?? '')
    );    
  }
}
