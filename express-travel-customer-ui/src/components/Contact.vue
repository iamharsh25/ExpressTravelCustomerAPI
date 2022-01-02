<template>
  <div>
    <h1>Contact Information</h1>
    <table border = "1" cellpadding = "5" cellspacing = "5">
      <tr>
        <th>ID</th>
        <th>Name</th>
        <th>DOB</th>
        <th>IsStaff</th>
      </tr>
      <tr v-for="c in contact" :key="c.contactId">
        <td>{{ c.contactId }}</td>
        <td>{{ getName(c) }}</td>
        <td>{{ formatDate(c.dob) }}</td>
        <td><input type="checkbox" id="isStaff" name="isStaff" :value="c.isStaff" v-model="c.isStaff" disabled></td>
        <td>
          <input type="button" value="Edit" @click="onEdit(c)"/>
          <input type="button" value="Delete" @click="onDelete(c.contactId)" />
        </td>
      </tr>
    </table>
  </div>
  <br />
  <br />
  <div>
    <form>
      <label for="search">Search: </label>
      <input type="text" v-model="searchId" placeholder="Enter ContactId" />
      <input type="button" value="Search" @click="onSearch"/>
      <br>
      <br>
      <label for="salutation">Salutation:</label>
      <select v-if="reference" v-model="entity.salutationId" name="salutation" id="salutation">
        <option v-for="s in getSalutation()"
        :key="s.referenceId"
        :value="s.referenceId">{{ s.refText }}
        </option>
      </select>
      <br>
      <br>
      <label for="fname">First name:</label>
      <input type="text" id="fname" name="fname" v-model="entity.firstName" /><br /><br />
      
      <label for="lname">Last name:</label>
      <input type="text" id="lname" name="lname" v-model="entity.lastName" /><br /><br />
      
      <label for="dob">DOB:</label>
      <input type="date" id="dob"
       name="dob" :max="getMaxDate()" v-model="entity.dob"><br /><br />

      <label for="isStaff">IsStaff:</label>
      <input type="checkbox" id="isStaff" name="isStaff" :value="entity.isStaff" v-model="entity.isStaff">
      
      <br>
      <br>
      <input v-if="isUpdate" type="button" value="Update" @click="onUpdate()" />
      <input v-else type="button" value="Insert" @click="onInsert()"/>
    </form>
  </div>
</template>

<script>
import ContactService from "../ContactService";

// Vue.use(VueAxios,axios)
export default {
  name: "Contact",
  data() {
    return {
      contact: [],
      reference: [],
      entity: {
        salutationId: 1,
        firstName: '',
        lastName: '',
        dob: this.formatDateCal(new Date()),
        isStaff: true
      },
      isUpdate: false,
      selectedContactId: 0,
      searchId: null
    };
  },
  created() {
    this.getContact();
    this.getReference();
  },
  methods: {
    getContact() {
      ContactService.getAllContacts()
        .then((response) => {
          this.contact = response.data;
          // console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    getReference() {
      ContactService.getAllReference()
        .then((response) => {
          this.reference = response.data;
          // console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    formatDate(datevalue) {
      var date = new Date(datevalue);
      return (
        (date.getDate() > 9 ? date.getDate() : "0" + date.getDate()) +
        "/" + 
        (date.getMonth() > 8
          ? date.getMonth() + 1
          : "0" + (date.getMonth() + 1)) +
        "/" +
        date.getFullYear()
      );
    },
    getName(entity) {
      var temp = this.reference.find(
        (s) => s.referenceId === entity.salutationId
      );
      if (temp)
        return temp.refText + " " + entity.firstName + " " + entity.lastName;
    },
    getSalutation() {
      return this.reference.filter((s) => s.refType === 'SAL')
    },
    onEdit(value) {
      var temp = this.reference.find(
        (s) => s.referenceId === value.salutationId
      );

      this.entity.salutationId = temp.referenceId
      this.entity.firstName = value.firstName
      this.entity.lastName = value.lastName
      this.entity.dob = this.formatDateCal(value.dob)
      this.entity.isStaff = value.isStaff
      this.selectedContactId = value.contactId
      this.isUpdate = true
    },
    onUpdate() {
      ContactService.putContact(this.selectedContactId, this.entity)
        .then((response) => {
          this.reference = response.data;
          // console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    onDelete(id) {
      ContactService.deleteContact(id)
        .then((response) => {
          this.reference = response.data;
          // console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    onInsert() {
      ContactService.postContact(this.entity)
        .then((response) => {
          this.reference = response.data;
          // console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    onSearch() {
      ContactService.getContactById(this.searchId)
        .then((response) => {
          this.entity = response.data;
          this.entity.dob = this.formatDateCal(this.entity.dob)
          this.selectedContactId = response.data.contactId
          // console.log(this.selectedContactId);
        })
        .catch(() => {
          alert("Contact Not found")
        });

        this.isUpdate = true;
    },
    formatDateCal(dateValue) {
      var date = new Date(dateValue);
      return (
        date.getFullYear() + "-" +
        (date.getMonth() > 8
          ? date.getMonth() + 1
          : "0" + (date.getMonth() + 1)) + "-" +
        (date.getDate() > 9 ? date.getDate() : "0" + date.getDate())
      );
    },
    getMaxDate() {
      var date = new Date();
      return (
        date.getFullYear() - 18 + "-" +
        (date.getMonth() > 8
          ? date.getMonth() + 1
          : "0" + (date.getMonth() + 1)) + "-" +
        (date.getDate() > 9 ? date.getDate() : "0" + date.getDate())
      );
    }
  },
};
</script>
<style>
</style>