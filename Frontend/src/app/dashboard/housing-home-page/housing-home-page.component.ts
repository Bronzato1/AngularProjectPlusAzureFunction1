import { Component, inject } from '@angular/core';
import { HousingService } from '../../services/housing-data-service';
import { IHousingLocation } from '../../interfaces/housing.location.interface';
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
  housingLocation: IHousingLocation | undefined;
  housingLocationList: IHousingLocation[] = [];

  constructor() {
    // Get #1
    this.housingService.getHousingLocationById(1).then((housingLocation: IHousingLocation | undefined) => {
      this.housingLocation = housingLocation;
    });
    // Get all
    this.housingService.getAllHousingLocations().then((housingLocationList: IHousingLocation[]) => {
      this.housingLocationList = housingLocationList;
    });
  }
}
