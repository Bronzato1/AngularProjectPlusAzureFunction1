import { Component, Input } from '@angular/core';
import { HousingLocation } from '../../Models/housing-location';

@Component({
  selector: 'app-housing-detail-page',
  standalone: true,
  templateUrl: './housing-detail-page.component.html',
  styleUrl: './housing-detail-page.component.scss'
})
export class HousingDetailPageComponent {
  @Input() housingLocation!: HousingLocation;
}
