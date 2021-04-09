import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [{
  id: 'applications',
  title: '',
  type: 'group',
  icon: 'apps',
  children: [{
      id: 'dashboard',
      title: 'Dashboard',
      translate: 'NAV.DASHBOARD.TITLE',
      type: 'item',
      icon: 'dashboard',
      url: '/workspace/dashboard'
    },
    {
      id: 'payments',
      title: 'payments',
      translate: 'NAV.PAYMENTS.TITLE',
      type: 'collapsable',
      icon: 'attach_money',
      children: [{
          id: 'payment-page',
          title: 'Payment',
          translate: 'NAV.PAYMENTS.NEW.TITLE',
          type: 'item',
          url: '/workspace/payments/new'
        },
        {
          id: 'payment-history',
          title: 'Payment history',
          translate: 'NAV.PAYMENTS.HISTORY.TITLE',
          type: 'item',
          url: '/workspace/payments/history'
        }
      ]
    },
    {
      id: 'statistic',
      title: 'Statistic',
      translate: 'NAV.STATISTIC.TITLE',
      type: 'item',
      icon: 'alarm',
      url: '/workspace/statistics'
    },
  ]
}];
