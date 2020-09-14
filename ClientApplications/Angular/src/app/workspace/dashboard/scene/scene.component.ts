import { Component, Input, OnInit } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Scene } from 'app/models/scene';
import { SessionStorageService } from 'app/services/session-storage.sevice';

// import { locale as english } from './i18n/en';
// import { locale as russian } from './i18n/ru';

@Component({
  selector: 'scene',
  templateUrl: './scene.component.html',
  styleUrls: ['./scene.component.scss']
})
export class SceneComponent implements OnInit {

  @Input()
  scene:Scene
  
  constructor()
  {
  }
  ngOnInit(): void {
  }

}
