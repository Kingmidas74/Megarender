import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Render } from '@DAL/api/models/entities/render';

import { Scene } from '@DAL/api/models/entities/scene';

@Component({
  selector: 'scene',
  templateUrl: './scene.component.html',
  styleUrls: ['./scene.component.scss']
})
export class SceneComponent implements OnInit, AfterViewInit, OnChanges {

  @Input()
  scene:Scene
  
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  dataSource: MatTableDataSource<Render>;
  
  displayedColumns: string[] = ['id', 'title', 'createdDate', 'status'];
  

  constructor()
  {
    
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    
  }

  ngOnInit(): void { 
  }


  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource(this.scene.renders);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;    
  }

}
