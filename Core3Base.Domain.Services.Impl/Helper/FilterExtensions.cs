using System.Linq;
using System.Linq.Dynamic.Core;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Filters;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Domain.Services.Impl.Helper
{
    public static class FilterExtensions
    {

        public static IQueryable<TSource> AddOrderAndPageFilters<TSource>(this System.Linq.IQueryable<TSource> input, FilterModelBase filter)
        {
            if (filter != null)
            {
                if (filter.OrderBy.Length > 0)
                {
                    input = input.OrderBy(filter.OrderBy);
                }
                else if (filter.OrderByDescending.Length > 0)
                {
                    input = input.OrderBy(filter.OrderByDescending + " DESC");
                }

                if (filter.Skip > 0 && filter.Take > 0)
                {
                    return input
                        .SkipIf(true, filter.Skip)
                        .TakeIf(true, filter.Take);
                }
                return input
                    .SkipIf(filter.Page > 0, filter.Page * filter.PageLimit)
                    .TakeIf(filter.PageLimit > 0, filter.PageLimit);
            }
            return input;

        }


        public static IQueryable<Blog> AddSearchFilters(this IQueryable<Blog> input, BlogFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Title.Contains(filter.Term));
                }

                if (filter.CategoryId >0)
                {
                    input = input.Where(x => x.CategoryId == filter.CategoryId);
                }
                if (filter.CategoryTerm?.Length > 0)
                {
                    input = input.Where(x => x.Category.Title.Contains(filter.CategoryTerm));
                }
                if (filter.CategoryNotTerm?.Length > 0)
                {
                    input = input.Where(x => !x.Category.Title.ToLower().Contains(filter.CategoryNotTerm.ToLower()));
                }
            }

            return input;

        }
        public static IQueryable<Student> AddSearchFilters(this IQueryable<Student> input, StudentFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Name.Contains(filter.Term));
                }
                if (filter.ClassId>0)
                {
                    input = input.Where(x => x.ClassId == filter.ClassId);
                }
                if (filter.NameTerm?.Length > 0)
                {
                    input = input.Where(x => x.Name.ToLower().Contains(filter.NameTerm.ToLower()));
                }
                if (filter.SurNameTerm?.Length > 0)
                {
                    input = input.Where(x => !x.Surname.ToLower().Contains(filter.SurNameTerm.ToLower()));
                }
            }

            return input;

        }


        public static IQueryable<Subjects> AddSearchFilters(this IQueryable<Subjects> input, SubjectFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Lesson.LessonName.Contains(filter.Term));
                }
                if (filter.SubjectDersIdTerm> 0)
                {
                    input = input.Where(x => x.LessonId== filter.SubjectDersIdTerm);
                }
               
            }

            return input;

        }

        public static IQueryable<Teacher> AddSearchFilters(this IQueryable<Teacher> input, TeacherFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Name.Contains(filter.Term));
                }

                if (filter.LessonId > 0)
                {
                    input = input.Where(x => x.LessonId == filter.LessonId);
                }
                if (filter.NameTerm?.Length > 0)
                {
                    input = input.Where(x => x.Name.ToLower().Contains(filter.NameTerm.ToLower()));
                }
                if (filter.SurNameTerm?.Length > 0)
                {
                    input = input.Where(x => !x.SurName.ToLower().Contains(filter.SurNameTerm.ToLower()));
                }
            }

            return input;

        }

        public static IQueryable<StudentAnswer> AddSearchFilters(this IQueryable<StudentAnswer> input, StudentAnswerFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.StudentId.ToString().Contains(filter.Term));
                }

            }

            return input;

        }

        public static IQueryable<LessonStudent> AddSearchFilters(this IQueryable<LessonStudent> input, LessonStudentFilterModel filter)
        {
            if (filter != null)
            {
            //    if (filter.Term?.Length > 0)
            //    {
            //        input = input.Where(x => x.Name.Contains(filter.Term));
            //    }

                if (filter.LessonId > 0)
                {
                    input = input.Where(x => x.LessonId == filter.LessonId);
                }

                if (filter.StudentId > 0)
                {
                    input = input.Where(x => x.StudentId == filter.StudentId);
                }
                //if (filter.NameTerm?.Length > 0)
                //{
                //    input = input.Where(x => x.Name.ToLower().Contains(filter.NameTerm.ToLower()));
                //}
                //if (filter.SurNameTerm?.Length > 0)
                //{
                //    input = input.Where(x => !x.SurName.ToLower().Contains(filter.SurNameTerm.ToLower()));

            }

            return input;

        }

        public static IQueryable<Classs> AddSearchFilters(this IQueryable<Classs> input, ClasssFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.ClassName.Contains(filter.Term));
                }

              
                if (filter.ClasssNameTerm?.Length > 0)
                {
                    input = input.Where(x => x.ClassName.ToLower().Contains(filter.ClasssNameTerm.ToLower()));
                }
               
            }

            return input;


        }


        public static IQueryable<Answer> AddSearchFilters(this IQueryable<Answer> input, AnswerFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Responce.Contains(filter.Term));
                }

            }

            return input;

        }

        public static IQueryable<QuestionExam> AddSearchFilters(this IQueryable<QuestionExam> input, QuestionExamFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Question.Content.Contains(filter.Term));
                }

            }

            return input;

        }

        public static IQueryable<Exam> AddSearchFilters(this IQueryable<Exam> input, ExamFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.ExamName.Contains(filter.Term));
                }

            }
            return input;

        }


        public static IQueryable<QuestionsAnswer> AddSearchFilters(this IQueryable<QuestionsAnswer> input, QuestionAnswerFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.QuestionsId.ToString().Contains(filter.Term));
                }


                if (filter.QuestionOptionId.ToString()?.Length > 0)
                {
                    input = input.Where(x => x.QuestionsId.ToString().Contains(filter.QuestionOptionId.ToString().ToLower()));
                }
            }
            return input;


        }


        public static IQueryable<Lesson> AddSearchFilters(this IQueryable<Lesson> input, LessonFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.LessonName.Contains(filter.Term));
                }

                if (filter.NameTerm?.Length > 0)
                {
                    input = input.Where(x => x.LessonName.ToLower().Contains(filter.NameTerm.ToLower()));
                }
            }

            return input;
        }

        public static IQueryable<Question> AddSearchFilters(this IQueryable<Question> input, QuestionFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Content.ToString().Contains(filter.Term));
                }
            }

            return input;
        }


        public static IQueryable<StudentQuestionAnswer> AddSearchFilters(this IQueryable<StudentQuestionAnswer> input, StudentQuestionFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.StudentId.ToString().Contains(filter.Term));
                }
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.QuestionId.ToString().Contains(filter.Term));
                }
            }

            return input;
        }



        public static IQueryable<BlogCategory> AddSearchFilters(this IQueryable<BlogCategory> input, BlogCategoryFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Title.Contains(filter.Term));
                }


                return input;
            }
            return input;

        }

        public static IQueryable<File> AddSearchFilters(this IQueryable<File> input, FileFilterModel filter)
        {
            if (filter != null)
            {
                if (filter.Term?.Length > 0)
                {
                    input = input.Where(x => x.Title.Contains(filter.Term));
                }


                return input;
            }
            return input;

        }


       
    }
}
