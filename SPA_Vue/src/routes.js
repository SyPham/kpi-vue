import Vue from "vue";
import VueRouter from "vue-router";

const Auth = () => import(/* webpackChunkName: "Auth" */ './components/auth/Auth.vue')
const Login = () => import(/* webpackChunkName: "Login" */ './components/auth/Login.vue')
const ForgotPassword = () => import(/* webpackChunkName: "ForgotPassword" */ './components/auth/Forgot-password.vue')
const RecoverPassword = () => import(/* webpackChunkName: "RecoverPassword" */ './components/auth/ConfimPassWord.vue')
const Dash = () => import(/* webpackChunkName: "Dash" */ './components/dash/Dash.vue')
const Home = () => import(/* webpackChunkName: "Home" */ './components/dash/Home.vue')
const HomeAdmin = () => import(/* webpackChunkName: "HomeAdmin" */ './components/dash/HomeAdmin.vue')

//AdminKPI
const AdminKPI = () => import(/* webpackChunkName: "AdminKPI" */ './views/AdminKPI/Index')
const UpdateKPI = () => import(/* webpackChunkName: "UpdateKPI" */ './views/AdminKPI/Update')
const CreateKPI = () => import(/* webpackChunkName: "CreateKPI" */ './views/AdminKPI/Create')

//AdminKPI_KIND
const AdminKPI_KIND = () => import(/* webpackChunkName: "AdminKPIKind" */ './views/AdminKPIKind/Index')
const UpdateKPI_KIND = () => import(/* webpackChunkName: "UpdateKPIKind" */ './views/AdminKPIKind/Update')
const CreateKPI_KIND = () => import(/* webpackChunkName: "CreateKPIKind" */ './views/AdminKPIKind/Create')

//AdminUsers
const AdminUser = () => import(/* webpackChunkName: "AdminUser" */ './views/AdminUser/Index')
const UpdateUser = () => import(/* webpackChunkName: "UpdateUser" */ './views/AdminUser/Update')
const CreateUser = () => import(/* webpackChunkName: "CreateUser" */ './views/AdminUser/Create')

//AdminCategory
const AdminCategory = () => import(/* webpackChunkName: "AdminCategory" */ './views/AdminCategory/Index')
const UpdateCategory = () => import(/* webpackChunkName: "UpdateCategory" */ './views/AdminCategory/Update')
const CreateCategory = () => import(/* webpackChunkName: "CreateCategory" */ './views/AdminCategory/Create')

//AdminMenu
const AdminMenu = () => import(/* webpackChunkName: "AdminMenu" */ './views/AdminMenu/Index')
const UpdateMenu = () => import(/* webpackChunkName: "UpdateMenu" */ './views/AdminMenu/Update')
const CreateMenu = () => import(/* webpackChunkName: "CreateMenu" */ './views/AdminMenu/Create')

//AdminOC
const AdminOC = () => import(/* webpackChunkName: "AdminOC" */ './views/AdminOC/Index')

//AddUserToLevel
const AddUserToLevel = () => import(/* webpackChunkName: "AddUserToLevel" */ './views/AddUserToLevel/Index')

//AddUserToLevel
const UserFavourite = () => import(/* webpackChunkName: "UserFavourite" */ './views/UserFavourite/Index')

//Overview
const UserOverview = () => import(/* webpackChunkName: "UserOverview" */ './views/Overview/Index')

//AdminLevel
const AdminLevel = () => import(/* webpackChunkName: "AdminLevel" */ './views/AdminLevel/Index')

//AdminCategoryOC
const AdminCategoryOC = () => import(/* webpackChunkName: "AdminCategoryOC" */ './views/AdminCategoryOC/Index')

//AdminOCCategoryKpi
const AdminOCCategoryKpi = () => import(/* webpackChunkName: "AdminOCCategoryKpi" */ './views/AdminOCCategoryKpi/Index')

//UserKPI
const UserKPI = () => import(/* webpackChunkName: "UserKPI" */ './views/UserKPI/Index')

//ChartPeriod
const ChartPeriod = () => import(/* webpackChunkName: "ChartPeriod" */ './views/ChartPeriod/Index')

//KPITrendView
const KPITrendView = () => import(/* webpackChunkName: "KPITrendView" */ './views/KPITrendView/Index')

//Datasets
const Dataset = () => import(/* webpackChunkName: "Dataset" */ './views/Datasets/Index')

//Datasets
const Workplace = () => import(/* webpackChunkName: "Workplace" */ './views/Workplace/Index')

//Compare
const Compare = () => import(/* webpackChunkName: "Compare" */ './views/Compare/Index')

//LateOnUpload
const LateOnUpload = () => import(/* webpackChunkName: "LateOnUpload" */ './views/LateOnUpload/Index')

//ListHistoryNotification
const ListHistoryNotification = () => import(/* webpackChunkName: "ListHistoryNotification" */ './views/ListHistoryNotification/Index')

//ChartPeriod/ListTasks
const ListTask = () => import(/* webpackChunkName: "ListTask" */ './views/ListTask/Index')

Vue.use(VueRouter);

const router = new VueRouter({
  // mode: 'history',
  routes: [
    {
      //HOME
      path: "/",
      component: Dash,
      redirect: "/home",
      meta: {
        breadcrumb: 'Home',
        url: "/home"
      },
      children: [

        // Home
        {
          path: "home",
          component: Home,
          meta: {
            requiresAuth: true
          }
        },

        // HomeAdmin
        {
          path: "home/admin",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: HomeAdmin,
              meta: {
                requiresAuth: true ,
              },
            }
          ]

        },

        // AdminKPI
        {
          path: "adminKPI",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminKPI ,
              meta: { requiresAuth: true, breadcrumb: 'KPI'},
            },
            {
              path: "create",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta: { requiresAuth: true, breadcrumb: 'KPI' , url: "/adminKPI"},
              children: [
                {
                  path: "",
                  component: CreateKPI ,
                  meta:{ requiresAuth: true , breadcrumb: 'Create KPI'},
                }
              ]
            },
            {
              path: ":id/edit",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta:{ requiresAuth: true , breadcrumb: 'KPI' , url: "/adminKPI"},
              children: [
                {
                  path: "",
                  component: UpdateKPI ,
                  meta:{ requiresAuth: true , breadcrumb: 'Update KPI'},
                }
              ]

            }
          ]
        },

        // AdminKPI_Kind
        {
          path: "KPIKind",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminKPI_KIND ,
              meta: { requiresAuth: true, breadcrumb: 'KPI KIND'},
            },
            {
              path: "create",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta: { requiresAuth: true, breadcrumb: 'KPI KIND' , url: "/KPIKind"},
              children: [
                {
                  path: "",
                  component: CreateKPI_KIND ,
                  meta:{ requiresAuth: true , breadcrumb: 'Create KPI KIND'},
                }
              ]
            },
            {
              path: ":id/edit",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta:{ requiresAuth: true , breadcrumb: 'KPI KIND' , url: "/KPIKind"},
              children: [
                {
                  path: "",
                  component: UpdateKPI ,
                  meta:{ requiresAuth: true , breadcrumb: 'Update KPI KIND'},
                }
              ]

            }
          ]
        },

        // AdminUser
        {
          path: "adminUser",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          // meta: { requiresAuth: true, breadcrumb: 'Admin User'},
          children: [
            {
              path: "",
              component: AdminUser ,
              meta: { requiresAuth: true, breadcrumb: 'User'},
            },
            {
              path: "create",
              component: CreateUser ,
              meta:{ requiresAuth: true , breadcrumb: 'Create User'},
            },
            {
              path: ":id/edit",
              component: UpdateKPI ,
              meta:{ requiresAuth: true , breadcrumb: 'Update User'},
            }
          ]
        },

        // AdminCategory
        {
          path: "adminCategory",
          name:"adminCategory",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminCategory ,
              meta: { requiresAuth: true, breadcrumb: 'Category' },
            },
            {
              path: "create",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta:{ requiresAuth: true , breadcrumb: 'Category' , url: "/adminCategory"},
              children: [
                {
                  path: "",
                  component: CreateCategory ,
                  meta:{ requiresAuth: true , breadcrumb: 'Create Category'},
                }
              ]

            },
            {
              path: ":id/edit",
              component: {
                render (c) {
                  return c('router-view')
                }
              },
              meta:{ requiresAuth: true , breadcrumb: 'Category' , url: "/adminCategory"},
              children: [
                {
                  path: "",
                  component: UpdateCategory ,
                  meta:{ requiresAuth: true , breadcrumb: 'Update Category'},
                }
              ]

            }
          ]
        },

        // AdminOC
        {
          path: "adminOC",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminOC,
              meta: {
                requiresAuth: true ,
                breadcrumb: 'KPI OC'
              },
            }
          ]

        },

        // AddUserToLevel
        {
          path: "AddUserToLevel",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AddUserToLevel,
              meta: {
                requiresAuth: true ,
                breadcrumb: 'Add User To Oc'
              },
            }
          ]
        },

        // AdminLevel
        {
          path: "adminLevel",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminLevel,
              meta: {
                requiresAuth: true ,
                breadcrumb: 'OC'
              },
            }
          ]
        },

        // OCCategories
        {
          path: "OCCategories",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminCategoryOC,
              meta: {
                requiresAuth: true ,
                breadcrumb: 'OC Category'
              },
            }
          ]
        },

        // CategoryKPILevelAdmin
        {
          path: "CategoryKPILevelAdmin",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Admin' , url: "/home/admin"},
          children: [
            {
              path: "",
              component: AdminOCCategoryKpi,
              meta: {
                requiresAuth: true ,
                breadcrumb: 'OC Category KPI'
              },
            }
          ]
        },

        // Favourite
        {
          path: "Favourite",
          component: UserFavourite,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'User Favourite'
          },
        },

        // Overview
        {
          path: "Overview",
          component: UserOverview,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'KPI Overview'
          },
        },

        // CategoryKPILevel
        {
          path: "CategoryKPILevel",
          component: UserKPI,
          meta: {
            requiresAuth: true ,
            url: "/CategoryKPILevel",
            breadcrumb: 'KPI'
          },
        },

        // ChartPeriod
        {
          path: "ChartPeriod",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'KPI' , url: "/CategoryKPILevel"},
          children: [
            {
              name: 'chart',
              path: ":kpilevelcode/:catid/:period/:year/:start/:end",
              component: ChartPeriod ,
              meta:{ requiresAuth: true , breadcrumb: 'Chart'},
            },
            {
              name: 'chart2',
              path: ":kpilevelcode/:catid/:period/:year/:start/:end/:type/:comID/:dataID/:title",
              component: ChartPeriod ,
              meta:{ requiresAuth: true , breadcrumb: 'Chart'},
            },

          ]
        },

        // KPITrendView
        {
          path: "KPITrendView",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'KPI Overview' , url: "/Overview"},
          children: [
            {
              path: ":kpilevelcode/:catid/:period/:year/:start/:end",
              component: KPITrendView ,
              meta:{ requiresAuth: true , breadcrumb: 'KPI Trend View'},
            }
          ]
        },

        // ChartPeriod/ListTasks
        {
          path: "ChartPeriod/ListTasks",
          component: ListTask,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'ChartPeriod ListTasks'
          },
        },

        // Dataset
        {
          path: "Dataset",
          component: {
            render (c) {
              return c('router-view')
            }
          },
          meta: { requiresAuth: true, breadcrumb: 'Category KPILevel' , url: "/CategoryKPILevel"},
          children: [
            {
              name: 'dataset',
              path: ":catid/:period/:start/:end/:year",
              component: Dataset ,
              meta:{ requiresAuth: true , breadcrumb: 'Dataset' },
            },
            {
              name: 'dataset2',
              path: ":kpilevelcode/:catid/:period/:year/:start/:end/:type/:comID/:dataID/:title",
              component: Dataset ,
              meta:{ requiresAuth: true },
            },

          ]
        },

        // Workplace
        {
          path: "Workplace",
          component: Workplace,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'Workplace'
          },
        },

        // Compare/:obj
        {
          path: "Compare/:obj",
          component: Compare,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'Compare'
          },
        },

        // LateOnUpload/:notification
        {
          path: "LateOnUpload/:notification",
          component: LateOnUpload,
          name: 'notification',
          meta: {
            requiresAuth: true ,
            breadcrumb: 'LateOnUpload'
          },
        },

        // ListHistoryNotification
        {
          path: "ListHistoryNotification",
          component: ListHistoryNotification,
          meta: {
            requiresAuth: true ,
            breadcrumb: 'List History Notification'
          },
        },

      ]
    },

    //Login
    {
      path: "/login",
      component: Auth,
      children: [
        { path: "/login", component: Login , meta: { requiresGuest: true} }

      ]
    },

    //Forgot-password
    {
      path: "/Forgot-Password",
      component: Auth,
      children: [
        { path: "", component: ForgotPassword , meta: { requiresGuest: true} }

      ]
    },

    //Forgot-password
    {
      path: "/recoverysuccess",
      component: Auth,
      children: [
        { path: "", component: RecoverPassword , meta: { requiresGuest: true} }

      ]
    },

  ]
});

export default router;

