//using System;

//namespace DataObjects.EntityFramework.oneContextOneDbSet
//{
//    /// <summary>
//    /// if we use one context one dbSet model, when dealing with mucl dbSet update at the same time case,
//    /// unit of work patter is need to put all dbSet into the same context as a whole transaciton
//    /// </summary>
//    public class UnitOfWork : IDisposable
//    {
//        private LMateEntities context = new LMateEntities();
//        private Repository<Department> departmentRepository;
//        private Repository<Course> courseRepository;

//        public Repository<Department> DepartmentRepository
//        {
//            get
//            {

//                if (this.departmentRepository == null)
//                {
//                    this.departmentRepository = new Repository<Department>(context);
//                }
//                return departmentRepository;
//            }
//        }

//        public Repository<Course> CourseRepository
//        {
//            get
//            {

//                if (this.courseRepository == null)
//                {
//                    this.courseRepository = new Repository<Course>(context);
//                }
//                return courseRepository;
//            }
//        }

//        public void Save()
//        {
//            context.SaveChanges();
//        }

//        private bool disposed = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    context.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }

//}