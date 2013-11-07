using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace nature_net
{
    public class worker_thread
    {
        task task_function;
        result_feedback result_function;
        object parameters;

        Thread worker;

        public worker_thread(task t, result_feedback r, object p)
        {
            this.task_function = t;
            this.result_function = r;
            this.parameters = p;
        }

        public void start_working()
        {
            if (worker == null)
            {
                worker = new Thread(new ThreadStart(this.work));
                worker.SetApartmentState(ApartmentState.STA);
                worker.Start();
            }
        }

        void work()
        {
            object info = this.task_function(parameters);
            this.result_function(info);
        }
    }

    public delegate void result_feedback(object info);
    public delegate object task(object parameteres);
}
