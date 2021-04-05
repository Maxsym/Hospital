using System;
using System.Collections.Generic;
using System.Text;
using Hospital.DAL.Domains;

namespace Hospital.BL.Responses
{
    public class CategoryResponse : BaseResponse<Category>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(Category category) : base(category)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(string message) : base(message)
        { }
    }
}
