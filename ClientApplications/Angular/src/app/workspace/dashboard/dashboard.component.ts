import { Component, OnInit } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Scene } from 'app/models/scene';
import { SessionStorageService } from 'app/services/session-storage.sevice';

import { locale as english } from './i18n/en';
import { locale as russian } from './i18n/ru';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  providers: [
   SessionStorageService,
   {provide: SessionStorageService.storageBaseKeyName, useValue: 'app-dashboard'},
 ]
})
export class DashboardComponent implements OnInit {

  constructor(
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private sessionStorageService: SessionStorageService<any>
  )
  {
      this._fuseTranslationLoaderService.loadTranslations(english, russian);
  }
  ngOnInit(): void {
  }

  scenes:Array<Scene> = [
    {
      id:'1',
      title:'1'
    },
    {
      id:'4',
      title:'3'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    },
    {
      id:'2',
      title:'2'
    }
  ]

}
