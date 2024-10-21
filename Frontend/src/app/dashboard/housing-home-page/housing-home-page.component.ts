import { Component, inject } from '@angular/core';
import { HousingService } from '../../services/housing.service';
import { HousingLocation } from '../../Models/housing-location';
import { HousingDetailPageComponent } from '../housing-detail-page/housing-detail-page.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-housing-home-page',
  standalone: true,
  imports: [CommonModule, HousingDetailPageComponent],
  templateUrl: './housing-home-page.component.html',
  styleUrl: './housing-home-page.component.scss'
})
export class HousingHomePageComponent {
  housingService = inject(HousingService);
  housingLocation: HousingLocation | undefined;
  housingLocationList: HousingLocation[] = [];

  constructor() {
    // Get #1
    this.housingService.getHousingLocationById(1).then((housingLocation: HousingLocation | undefined) => {
      this.housingLocation = housingLocation;
    });
    // Get all
    this.housingService.getAllHousingLocations().then((housingLocationList: HousingLocation[]) => {
      this.housingLocationList = housingLocationList;
    });
  }
}
