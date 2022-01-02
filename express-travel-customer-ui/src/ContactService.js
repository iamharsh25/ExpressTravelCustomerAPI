import http from "./http-comman";
class ContactService {

    getAllContacts() {
        return http.get("api/Contact");
    }

    getAllReference() {
        return http.get("api/Reference");
    }

    getContactById(id) {
        return http.get("api/Contact/" + id);
    }

    putContact(id, entity) {
        return http.put("api/Contact/" + id, JSON.stringify(entity));
    }

    postContact(entity) {
        return http.post("api/Contact", JSON.stringify(entity));
    }

    deleteContact(id) {
        return http.delete("api/Contact/" + id);
    }
}
export default new ContactService();  