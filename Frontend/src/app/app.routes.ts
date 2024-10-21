import { Routes } from '@angular/router';
import { OktaAuthGuard, OktaCallbackComponent } from '@okta/okta-angular';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TestPageComponent } from './dashboard/test-page/test-page.component';
import { SecuredPageComponent } from './dashboard/secured-page/secured-page.component';
import { BlankPageComponent } from './dashboard/blank-page/blank-page.component';
import { HousingHomePageComponent } from './dashboard/housing-home-page/housing-home-page.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'logout/callback', component: OktaCallbackComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: 'test-page', pathMatch: 'full' },
      { path: 'test-page', component: TestPageComponent },
      { path: 'secured-page', component: SecuredPageComponent, canActivate: [OktaAuthGuard] },
      { path: 'blank-page', component: BlankPageComponent },
      { path: 'housing-home-page', component: HousingHomePageComponent }
    ]
}
];