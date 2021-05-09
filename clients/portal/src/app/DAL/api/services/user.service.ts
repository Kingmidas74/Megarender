import { Injectable } from '@angular/core';
import { ApiHttpService } from '../api-interceptor';
import { CreateUserCommand } from '../models/commands/create-user-command'
import { User } from '../models/entities/user'
import { Observable } from 'rxjs';
import * as uuid from 'uuid';

@Injectable()
export class UserService {  

  constructor(private apiHttpClient:ApiHttpService) { }

  public createUser(createUserCommand:CreateUserCommand):Observable<User>
  {
    createUserCommand.commandId = uuid.v4();
    return this.apiHttpClient.post<User>('/users/',createUserCommand);
  }

  public getUserById(userId:string): Observable<User> {
    return this.apiHttpClient.get<User>(`/users/${userId}`);
  }
}
