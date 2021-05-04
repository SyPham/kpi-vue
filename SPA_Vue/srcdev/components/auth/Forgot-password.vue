<template>
    <div class="limiter">
      <div class="container-login100">
        <div class="wrap-login100 p-l-55 p-r-55 p-t-65 p-b-50">

            <span class="login100-form-title p-b-33">
               KPI System
            </span>
            <p class="login-box-msg">You forgot your password? Here you can easily retrieve a new password.</p>
             <form @submit.prevent="resetPassW">   
                <div class="input-group mb-3">
                    <input type="text" class="form-control" :class="{ 'is-invalid': submitted && $v.email.$error }" v-model="email" placeholder="Email">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                    <div v-if="submitted && !$v.email.required" class="invalid-feedback">Email is required</div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <button  class="btn btn-primary btn-block">Request new password</button>
                    </div>
                </div>
             </form>
            <p class="mt-3 mb-1">
                <a href="#/login?redirect=%2Fhome">Back to Login</a>
            </p>

        </div>
      </div>
    </div>
 
</template>
<script>
import { required, email, minLength, sameAs } from "vuelidate/lib/validators";
export default {
    data() {
    return {
      email: '',
      submitted: false
    }
  },
  validations:{
      email: { required }
  },

  created(){


  },
  methods: {
   resetPassW(e){
    //    console.log('aaaa')
       let mObj = {
           Email: this.email
       }
       this.submitted = true;
       this.$v.$touch();
        if (this.$v.$invalid) {
            return;
        }
        axios.post('AdminUser/RecoverPassword',mObj).then((result) => {
            if(result.data){
                success("Recover successfully!");
                this.$router.push('/recoverysuccess')
            }
            else{
                error("Email not exist! Please try again");
            }
        }).catch((err) => {
                
        });
   }
  }
}
</script>