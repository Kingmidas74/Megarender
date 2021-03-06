import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-identity-layout',
  templateUrl: './identity-layout.component.html',
  styleUrls: ['./identity-layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class IdentityLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
