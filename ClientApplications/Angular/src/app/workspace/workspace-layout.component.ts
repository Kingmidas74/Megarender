import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { takeUntil, map, tap } from 'rxjs/operators';

import { FuseConfigService } from '@fuse/services/config.service';
import { navigation } from 'app/workspace/navigation/navigation';
import { User } from '@DAL/api/models/entities/user';
import { StorageMap } from '@ngx-pwa/local-storage';
import { environment } from 'environments/environment';
import { AuthenticationService } from '../identity/services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workspace-layout',
  templateUrl: './workspace-layout.component.html',
  styleUrls: ['./workspace-layout.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class WorkspaceLayoutComponent implements OnInit, OnDestroy
{
    fuseConfig: any;
    navigation: any;

    currentUser$:Observable<User>;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private storage: StorageMap,
        private authenticationService: AuthenticationService,
        private router: Router
    )
    {
        // Set the defaults
        this.navigation = navigation;

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
                this.fuseConfig = config;
            });
        this.currentUser$ = this.storage.get<User>(environment.constants.UserStorageKey).pipe(map(user=>user as User));
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    logout() {
        this.authenticationService.logout().subscribe(
            _=>this.router.navigate(['identity'])
        );
    }
}
