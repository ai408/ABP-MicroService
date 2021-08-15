<template>
  <div class="app-container">
    <div class="mixin-components-container">
      <el-row>
        <el-card class="box-card" style="text-align:left" >
          <div slot="header" class="clearfix title">
            <span>播放列表音频可视化演示例子</span>
          </div>
          <div id="waveform" ref="waveform">
          </div>
          <div id="wave-timeline" ref="wave-timeline">
          </div>
          <div style="padding-top: 10px">
            <el-button type="primary" @click="rew">后退</el-button>
            <el-button type="primary" @click="playMusic">
              <i class="el-icon-video-play"></i>
              播放 /
              <i class="el-icon-video-pausee"></i>
              暂停
            </el-button>
            <el-button type="primary" @click="speek">前进</el-button>
          </div>
        </el-card>
      </el-row>
    </div>

    <el-table
      :data="tableData"
      style="width: 100%">
      <el-table-column
        label="文件"
        width="360">
        <template slot-scope="scope">
          <i class="el-icon-caret-left"></i>
          <span style="margin-left: 10px">{{ scope.row.date }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="标签"
        width="360">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>姓名: {{ scope.row.name }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.name }}</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button
            size="mini"
            type="info"
            @click="handleChange(scope.$index, scope.row)">切换</el-button>
          <el-button
            size="mini"
            type="info"
            @click="handlePredict(scope.$index, scope.row)">预测</el-button>
        </template>
      </el-table-column>
    </el-table>

  </div>
</template>
<script>
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
import WaveSurfer from 'wavesurfer.js'
import Timeline from 'wavesurfer.js/dist/plugin/wavesurfer.timeline.js'
const defaultForm = {
  id: null,
  name: null,
  description: null,
  price: null,
}
export default {
  name: 'Video',
  components: {
    Pagination
  },
  directives: {
    permission
  },
  props: [],
  data() {
    return {
      rules: {
        name: [],
        description: [],
        price: [],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        FilterName: '',
        FilterDescription: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      wavesurfer: null,
      tableData: [{
        date: '2016-05-02',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1518 弄'
      }, {
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄'
      }, {
        date: '2016-05-01',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1519 弄'
      }, {
        date: '2016-05-03',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1516 弄'
      }]
    }
  },
  computed: {},
  watch: {},
  created() {
    this.getList();
  },
  mounted() {
    this.$nextTick(() => {
      console.log(WaveSurfer)
      this.wavesurfer = WaveSurfer.create({
        container: this.$refs.waveform,
        waveColor: '#409EFF',
        progressColor: 'blue',
        backend: 'MediaElement',
        mediaControls: false,
        audioRate: '1',
        //使用时间轴插件
        plugins: [
          Timeline.create({
            container: '#wave-timeline'
          }),
        ]
      });
      // 特别提醒：此处需要使用require(相对路径)，否则会报错
      this.wavesurfer.load(require('./peaks/sample.mp3'));
    })
  },
  methods: {
      playMusic() {
        //"播放/暂停"按钮的单击触发事件，暂停的话单击则播放，正在播放的话单击则暂停播放
        this.wavesurfer.playPause.bind(this.wavesurfer)()
      },
      // 后退
      rew() {
        this.wavesurfer.skip(-3)
      },
      // 前进
      speek() {
        this.wavesurfer.skip(3)
      },
      handleChange(index, row) {
        if (index === 0)
          this.wavesurfer.load(require('./peaks/demo.wav'));
        if (index === 1)
          this.wavesurfer.load(require('./peaks/001z.mp3'));
        if (index === 2)
          this.wavesurfer.load(require('./peaks/media.wav'));
      },
      handlePredict(index, row) {
        console.log(index, row);
      },
      getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('/api/business/book', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('/api/business/book/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增Book';
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    // handleDelete(row) {
    //   var params = [];
    //   let alert = '';
    //   if (row) {
    //     params.push(row.id);
    //     alert = row.name;
    //   }
    //   else {
    //     if (this.multipleSelection.length === 0) {
    //       this.$message({
    //         message: '未选择',
    //         type: 'warning'
    //       });
    //       return;
    //     }
    //     this.multipleSelection.forEach(element => {
    //       let id = element.id;
    //       params.push(id);
    //     });
    //     alert = '选中项';
    //   }
    //   this.$confirm('是否删除' + alert + '?', '提示', {
    //     confirmButtonText: '确定',
    //     cancelButtonText: '取消',
    //     type: 'warning'
    //   }).then(() => {
    //     this.$axios.posts('/api/business/book/delete', params).then(response => {
    //       this.$notify({
    //         title: '成功',
    //         message: '删除成功',
    //         type: 'success',
    //         duration: 2000
    //       });
    //       this.getList();
    //     });
    //   }).catch(() => {
    //     this.$message({
    //       type: 'info',
    //       message: '已取消删除'
    //     });
    //   });
    // },
    handleUpdate(row) {
      this.formTitle = '修改Book';
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      }
      else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: '编辑必须选择单行',
            type: 'warning'
          });
          return;
        }
        else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.puts('/api/business/book/' + this.form.id, this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
          else {
            this.$axios.posts('/api/business/book', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '新增成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
        }
      });
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + ' ' + order;
      this.handleFilter();
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleRowClick(row, column, event) {
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
  }
}
</script>

<style lang="scss" scoped>
.page {
  display: block;
  text-align: right;
  padding: 5px 2px 0 0;
}

.mixin-components-container {
  background-color: #f0f2f5;
  padding: 10px;
}

</style>
