<template>
  <v-container grid-list-sm fluid>
    <v-layout row wrap>
      <v-flex xs4 v-for="photo in photos" :key="photo.id" d-flex>
        <v-hover>
          <v-card
            ma-1
            flat
            tile
            slot-scope="{ hover }"
            :class="`elevation-${hover ? 12 : 2}`"
            max-width="180px"
            max-heigth="100px"
          >
            <v-img :src="photo.url" :lazy-src="photo.url" aspect-ratio="1"></v-img>
            <v-card-actions>
              <v-btn :disabled="photo.isMain" flat color="orange" @click="setMainPhoto(photo)">
                <v-icon>assignment_turned_in</v-icon>
              </v-btn>
              <v-btn flat color="orange" @click="deletePhoto(photo)">
                <v-icon>delete</v-icon>
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-hover>
      </v-flex>
      <v-flex xs12>
        <v-card>
          <v-card-title primary-title>
            <ul v-if="files.length">
              <li v-for="file in files" :key="file.id">
                <span>{{file.name}}</span> -
                <span>{{file.size}}</span> -
                <span v-if="file.error">{{file.error}}</span>
                <span v-else-if="file.success">success</span>
                <span v-else-if="file.active">active</span>
                <span v-else></span>
              </li>
            </ul>
            <ul v-else>
              <div class="text-center p-5">
                <h4>Drop files anywhere to upload
                  <br>or
                </h4>
                <label for="file" class="btn btn-lg btn-primary">Select Files</label>
              </div>
            </ul>
            <div v-show="$refs.upload && $refs.upload.dropActive" class="drop-active">
              <h3>Drop files to upload</h3>
            </div>
          </v-card-title>
          <v-card-actions>
            <file-upload
              class="v-btn"
              :custom-action="customAction"
              :multiple="true"
              :drop="true"
              :drop-directory="true"
              :size="1024 * 1024 * 10"
              v-model="files"
              @input-filter="inputFilter"
              @input-file="inputFile"
              ref="upload"
            >Select files</file-upload>
            <v-btn
              color="success"
              v-if="!$refs.upload || !$refs.upload.active"
              @click.prevent="$refs.upload.active = true"
            >Start Upload</v-btn>
            <v-btn color="error" v-else @click.prevent="$refs.upload.active = false">Stop Upload</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import UserService from "../../services/userService.js";
import FileUpload from "vue-upload-component";
export default {
  props: ["photos"],
  components: {
    FileUpload
  },
  data() {
    return {
      files: [],
      currentMain: null
    };
  },
  computed: {
    userId() {
      return this.$store.getters.userId;
    }
  },
  methods: {
    inputFilter(newFile, oldFile, prevent) {
      if (newFile && !oldFile) {
        // Before adding a file
        // 添加文件前
        // Filter system files or hide files
        // 过滤系统文件 和隐藏文件
        if (/(\/|^)(Thumbs\.db|desktop\.ini|\..+)$/.test(newFile.name)) {
          return prevent();
        }
        // Filter php html js file
        // 过滤 php html js 文件
        if (/\.(php5?|html?|jsx?)$/i.test(newFile.name)) {
          return prevent();
        }
      }
    },
    inputFile(newFile, oldFile) {
      if (newFile && !oldFile) {
        // add
        console.log("add", newFile);
      }
      if (newFile && oldFile) {
        // update
        console.log("update", newFile);
      }
      if (!newFile && oldFile) {
        // remove
        console.log("remove", oldFile);
      }
    },
    customAction(file, component) {
      console.log(file);
      UserService.addPicture(this.userId, {  file, description:"test" })
        .then(rep => console.log(rep))
        .catch(err => console.log(err));
    },
    setMainPhoto(photo) {
      UserService.setPictureMain(this.userId, photo.id)
        .then(resp => {
          this.currentMain = this.photos.filter(p => p.isMain === true)[0];
          this.currentMain.isMain = false;
          photo.isMain = true;
          this.$emit("photoChange", photo);
          this.$alertify.success("Profile update successfully");
        })
        .catch(err => this.$alertify.error(err));
    },
    deletePhoto(photo) {
      UserService.deletePicture(this.userId,photo.id) .then(resp => {  
        this.$alertify.success("Delete photo successfully");
         this.$emit("photoDelete", photo);
        })
        .catch(err => this.$alertify.error(err));
    }
  }
};
</script>

