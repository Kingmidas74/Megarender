import { Injectable, Inject } from "@angular/core";

@Injectable()
export class SessionStorageService<T> {

  public static storageBaseKeyName = 'storageBaseKey'

  private generateKey(itemsKey:string) {
    return `${this.storageBaseKey}-${itemsKey}` 
  }

  constructor(@Inject(SessionStorageService.storageBaseKeyName) private storageBaseKey:string) {}

  setItems(key:string, items:T[] | T) {    
    if (items != null) {
      sessionStorage.setItem(this.generateKey(key), JSON.stringify(items));
    }
  }

  getItems(key:string):T[] | T {
    let items = sessionStorage.getItem(this.generateKey(key));    
    if (
      typeof items !== "undefined" &&
      items != "" &&
      items != null
    ) {
      let fromStorage: string[] = JSON.parse(items);
      if (fromStorage != null) {
        if (fromStorage.length > 0) {
          return JSON.parse(items);
        }
      }
    }
    return new Array<T>();
  }

}