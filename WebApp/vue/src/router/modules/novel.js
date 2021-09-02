/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const novelRouter = {
  path: '/novel',
  component: Layout,
  redirect: 'noRedirect',
  name: 'novel',
  meta: {
    title: '小说操作',
    icon: 'component'
  },
  children: [
    {
      path: 'novel',
      component: () => import('@/views/novel/index'),
      name: 'novel',
      meta: { title: '小说操作' }
    },
  ]
}

export default novelRouter
