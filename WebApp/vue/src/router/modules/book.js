/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const bookRouter = {
  path: '/book',
  component: Layout,
  redirect: 'noRedirect',
  name: 'book',
  meta: {
    title: '书籍操作',
    icon: 'component'
  },
  children: [
    {
      path: 'book',
      component: () => import('@/views/book/index'),
      name: 'book',
      meta: { title: '书籍操作' }
    },
  ]
}

export default bookRouter
