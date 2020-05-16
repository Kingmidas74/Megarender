import { Injectable } from '@angular/core';
import { ApiHttpService } from '../api-interceptor';
import { CreateUserCommand } from '../models/commands/create-user-command'
import { User } from '../models/entities/user'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MegarenderApiService {

  constructor(private apiHttpClient:ApiHttpService) { }

  public createUser(createUserCommand:CreateUserCommand):Observable<User>
  {
    return this.apiHttpClient.post<User>('/users',createUserCommand);
  }
}
