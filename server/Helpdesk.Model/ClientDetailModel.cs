﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Helpdesk.Model
{
    public class ClientDetailModel
    {
        /// <summary>
        /// The Id for this client
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the client
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the client
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The phone number of the client
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The email address of the client
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The company of the client
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The address of the client
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The date that the client was added to the system
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}