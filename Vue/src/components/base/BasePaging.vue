<template>
    <div class="pagination pointer">
        <button class="page-prev" @click="onClickPrevPage" :disabled="isInFirstPage">Trước</button>
        <div class="page-index">
            <button class="page-number" v-for="(page, index) in pages" :key="index" @click="onClickPage(page.number)" :class="{selected: isPageSelect(page.number)}"  :disabled="page.isDisabled" >{{page.number}}</button>
        </div>
        <button class="page-next" @click="onClickNextPage" :disabled="isInLastPage" >Sau</button>
    </div>
</template>
<script>
export default {
    props: ['totalPages', 'total', 'perPage', 'currentPage'],
    data() {
        return {
            maxVisibleButtons: 6
        }   
    },
    computed: {
        /**
         * Lê Minh Tuyến (24/7/2022)
         * lấy button page đầu tiên
         */
        startPage() {
            if (this.currentPage === 1) return 1
            // if (this.currentPage === this.totalPages) return this.totalPages - this.maxVisibleButtons + (this.maxVisibleButtons -1)
            if(this.currentPage < this.maxVisibleButtons - 1 ) return 1
            return this.currentPage - this.maxVisibleButtons + 2
        },
        /**
         * Lê Minh Tuyến (24/7/2022)
         * lấy button page cuối cùng
         */
        endPage() {
            return Math.min(this.startPage + this.maxVisibleButtons - 1, this.totalPages) 
        },
        /**
         * Lê Minh Tuyến (24/7/2022)
         * Hiển thị các nút paging
         */
        pages() {
            let range = []
            for (let i = this.startPage; i <= this.endPage; i+= 1 ) {
                range.push({
                number: i,
                isDisabled: i === this.currentPage 
                });
            }
            return range
        },
        isInFirstPage() { return this.currentPage === 1 },
        isInLastPage() { return this.currentPage === this.totalPages },
    },
    methods: {
        /**
         * Lê Minh Tuyến (24/7/2022)
         * về trang trước
         */
        onClickPrevPage() {
            this.$emit('pageChanged', this.currentPage - 1)
        },
        /**
         * Lê Minh Tuyến (24/7/2022)
         * chọn trang bất kỳ
         */
        onClickPage(page) {
            this.$emit('pageChanged', page)
        },
        /**
         * Lê Minh Tuyến (24/7/2022)
         * đến trang sau
         */
        onClickNextPage() {
            this.$emit('pageChanged', this.currentPage + 1)
        },
        /**
         * Lê Minh Tuyến (24/7/2022)
         * chọn trang
         */
        isPageSelect(page) {
             return this.currentPage === page
        },
        // onPageChange(page) {
        //     this.$emit('pageChanged', page);
        //     this.currentPage = page;
        // }
    } 
}
</script>
<style scoped>
     @import url(../../assets/css/page/content/paging.css);
</style>