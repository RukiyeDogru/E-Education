using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Impl.Services;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data;
using Core3Base.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Core3Base.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IClasssService, ClasssService>();
            services.AddScoped<ILessonStudentService, LessonStudentService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IStudentAnswerService, StudentAnswerService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionAnswerService, QuestionsAnswerService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IStudentQuestionAnswerService, StudentQuestionAnswerService>();
            services.AddScoped<IQuestionExamService, QuestionExamService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<Core3BaseContext>();
        }
        public static void RegisterFileHelper(IServiceCollection services, string FileServerUrl)
        {
            services.AddScoped<ImageHelper>(
                s => new ImageHelper(s.GetRequiredService<IFileService>(),
                    FileServerUrl));

            services.AddScoped<FileHelper>(
                s => new FileHelper(s.GetRequiredService<IFileService>(),
                    FileServerUrl));
        }
    }
}
