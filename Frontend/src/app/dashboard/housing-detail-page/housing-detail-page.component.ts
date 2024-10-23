import { Component, Input } from '@angular/core';
import { IHousingLocation } from '../../interfaces/housing.location.interface';

@Component({
  selector: 'app-housing-detail-page',
  standalone: true,
  templateUrl: './housing-detail-page.component.html',
  styleUrl: './housing-detail-page.component.scss'
})
export class HousingDetailPageComponent {
  @Input() housingLocation!: IHousingLocation;
}
