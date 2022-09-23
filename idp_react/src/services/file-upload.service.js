import http from "../http-common";

class UploadFilesService {
  upload(file, onUploadProgress) {
    let formData = new FormData();
debugger
    formData.append("files", file);
    formData.append("createdBy","Jaspreet");
    console.log("formData", formData)

    return http.post("/Document/UploadInvoices", formData, {
      mode: "cors",
      headers: {
        "Content-Type": "multipart/form-data",
      },
      onUploadProgress,
    });
  }

  getFiles() {
    return http.get("/Document");
  }
}

export default new UploadFilesService();