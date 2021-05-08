import {SubscriptionLike as ISubscription, Subscription} from 'rxjs';

/**
 * Unsubscribes from all available subscriptions in component
 * @returns
 */
export function CleanSubscriptions(): Function {
  return function (constructor: Function) {
    const original = constructor.prototype.ngOnDestroy;
    if (typeof original !== 'function') {
      console.warn(`OnDestroy is not implemented in ${constructor.name} but it's using @CleanSubscriptions decorator`);
    }
    constructor.prototype.ngOnDestroy = function () {
      for (const prop in this) {
        const item: ISubscription | any = this[prop];
        if (item) {
          if (Array.isArray(item) && item.length > 0 && item[0] instanceof Subscription) {
            item.forEach(sub => cleanSubscription(sub));
          } else {
            cleanSubscription(item);
          }
        }
      }
      original.apply(this, arguments);
    };
  };
}

/**
 * Unsubscribe from observable if not completed
 * @param sub
 */
export function cleanSubscription(sub: any): void {
  if (sub instanceof Subscription && !sub.closed) {
    sub.unsubscribe();
  }
}
