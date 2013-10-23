namespace CrystalQuartz.Web.Processors
{
    using Core;
    using FrontController;
    using FrontController.ViewRendering;

    public class JobLogsFiller : MasterFiller
    {
        public JobLogsFiller(IViewEngine viewEngine, ISchedulerDataProvider schedulerDataProvider)
            : base(viewEngine, schedulerDataProvider)
        {
        }

        protected override void FillViewData(ViewData viewData)
        {
            var jobName = Request.Params["job"];
            var jobGroup = Request.Params["group"];
            viewData.Data["mainContent"] = "jobLogs";
            viewData.Data["jobDetails"] = _schedulerDataProvider.GetJobDetailsData(jobName, jobGroup);
            viewData.Data["jobLogs"] = _schedulerDataProvider.GetJobLogs(jobName, jobGroup);
        }
    }
}