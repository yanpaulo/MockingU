import { createApp } from 'vue'

createApp({
    data() {
        return {
            method: '',
            content: '',
            defaultMethods: ['GET', 'POST', 'PUT', 'DELETE'],
            methods: [],
            contents: []
        }
    }
    ,
    methods: {
        addMethod(method) {
            var value = method || this.method;
            if (!value) {
                return;
            }
            if (this.methods.includes(value)) {
                return;
            }
            this.methods.push(value);
            if (!method) {
                this.method = null;
            }
        },
        removeMethod(index) {
            this.methods.splice(index, 1);
        },
        addContent() {
            if (!this.content) {
                return;
            }
            this.contents.push(this.content);
            this.content = null;
        },
        removeContent(index) {
            this.contents.splice(index, 1);
        }
    },
    watch: {
        method(newVal) {
            this.method = (this.method || '').toUpperCase()
        }
    },
    mounted() {
        this.methods = Array.from(document.querySelectorAll(".hidden-method")).map(e => e.value);
        this.contents = Array.from(document.querySelectorAll(".hidden-content")).map(e => e.value);

        var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
        popoverTriggerList.map(function (popoverTriggerEl) {
            return new bootstrap.Popover(popoverTriggerEl)
        });
    }
}).mount('#app');


