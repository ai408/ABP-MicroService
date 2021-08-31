/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const videoRouter = {
  path: '/video',
  component: Layout,
  redirect: 'noRedirect',
  name: 'video',
  meta: {
    title: '预测模型',
    icon: 'component'
  },
  children: [
    {
      path: 'video',
      component: () => import('@/views/video/index'),
      name: 'video',
      meta: { title: '音频数据' }
    },
    {
      path: 'temhum',
      component: () => import('@/views/temhum/index'),
      name: 'temhum',
      meta: { title: '数值数据' }
    }
  ]
}

export default videoRouter
