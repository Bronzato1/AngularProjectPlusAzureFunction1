import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass } from '@angular/common';
import { ToggleService } from '../header/toggle.service';
import { CustomizerSettingsService } from '../../customizer-settings/customizer-settings.service';
import { OktaAuth, AuthState } from '@okta/okta-auth-js';
import { OKTA_AUTH, OktaAuthStateService } from '@okta/okta-angular';

interface MenuItem {
    title: string;
    subItems?: MenuItem[];
}

@Component({
    selector: 'app-sidebar',
    standalone: true,
    imports: [NgScrollbarModule, RouterLinkActive, RouterLink, NgClass, CommonModule],
    templateUrl: './sidebar.component.html',
    styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit {

    // isSidebarToggled
    isSidebarToggled = false;

    // isToggled
    isToggled = false;

    // Okta
    isAuthenticated: boolean = false;

    constructor(
        private toggleService: ToggleService,
        public themeService: CustomizerSettingsService,
        @Inject(OKTA_AUTH) public oktaAuth: OktaAuth,
        private oktaAuthStateService: OktaAuthStateService,
    ) {
        this.toggleService.isSidebarToggled$.subscribe(isSidebarToggled => {
            this.isSidebarToggled = isSidebarToggled;
        });
        this.themeService.isToggled$.subscribe(isToggled => {
            this.isToggled = isToggled;
        });
    }

    // Burger Menu Toggle
    toggle() {
        this.toggleService.toggle();
    }

    // Accordion
    openSectionIndex: number = -1;
    openSectionIndex2: number = -1;
    openSectionIndex3: number = -1;
    toggleSection(index: number): void {
        if (this.openSectionIndex === index) {
            this.openSectionIndex = -1;
        } else {
            this.openSectionIndex = index;
        }
    }
    toggleSection2(index: number): void {
        if (this.openSectionIndex2 === index) {
            this.openSectionIndex2 = -1;
        } else {
            this.openSectionIndex2 = index;
        }
    }
    toggleSection3(index: number): void {
        if (this.openSectionIndex3 === index) {
            this.openSectionIndex3 = -1;
        } else {
            this.openSectionIndex3 = index;
        }
    }
    isSectionOpen(index: number): boolean {
        return this.openSectionIndex === index;
    }
    isSectionOpen2(index: number): boolean {
        return this.openSectionIndex2 === index;
    }
    isSectionOpen3(index: number): boolean {
        return this.openSectionIndex3 === index;
    }

    async ngOnInit()
    {
        this.isAuthenticated = await this.oktaAuth.isAuthenticated();
        this.oktaAuthStateService.authState$.subscribe(
            (response) => { this.isAuthenticated = response.isAuthenticated ?? false; } ); 
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